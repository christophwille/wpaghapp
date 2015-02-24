using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web;
using Newtonsoft.Json;
using Octokit;

namespace WpaGhApp.Github
{
    // Based off of https://gist.github.com/nigel-sampson/8566723
    public partial class GhService
    {
        private const string VaultDefaultUnusedUserName = "fakeusername";
        private const string VaultStateResourceKey = "state";
        private const string VaultTokenCodeResourceKey = "tokencode";
        private const string VaultTokenInfoResourceKey = "accesstokeninfo";

        private string DeclareScopes()
        {
            // See https://developer.github.com/v3/oauth/#scopes
            return "user,repo";
        }

        public string FormatGitHubLoginUrl(string clientId, string redirectUri, string scopes, string state)
        {
            return String.Format("https://github.com/login/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}&state={3}",
                        clientId,
                        redirectUri,
                        scopes,
                        state);
        }

        public async Task StartOAuthFlow()
        {
            Exception exIfAny = null;

            try
            {
                string state = Guid.NewGuid().ToString("N");
                VaultManager.Save(VaultStateResourceKey, VaultDefaultUnusedUserName, state);

                // Delete the now "old" code and access token info record from the vault before proceeding
                VaultManager.Delete(VaultTokenCodeResourceKey, VaultDefaultUnusedUserName);
                VaultManager.Delete(VaultTokenInfoResourceKey, VaultDefaultUnusedUserName);
                _gitHubClient.Credentials = Credentials.Anonymous;

                var uri = FormatGitHubLoginUrl(GhOAuthConfiguration.ClientId,
                            GhOAuthConfiguration.RedirectUri,
                            DeclareScopes(),
                            state);

                Uri requestUri = new Uri(uri);
                Uri callbackUri = new Uri(GhOAuthConfiguration.RedirectUri);

                WebAuthenticationBroker.AuthenticateAndContinue(requestUri,
                    callbackUri,
                    null,
                    WebAuthenticationOptions.None);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                exIfAny = ex;
            }

            if (null != exIfAny)
            {
                await _messageService.ShowAsync(exIfAny.Message);
            }
        }

        public async Task<bool> CompleteOAuthFlowAsync(string responseData)
        {
            // responseData value eg http://example.com/path?code=4cba03187d6cd6720217&state=6dfa60685ac94a29bdde2d9f3cbdce0e
            var responseUri = new Uri(responseData);
            var decoder = new WwwFormUrlDecoder(responseUri.Query);
            var code = decoder.GetFirstValueByName("code");
            var state = decoder.GetFirstValueByName("state");

            string storedState = VaultManager.GetPassword(VaultStateResourceKey, VaultDefaultUnusedUserName);
            if (storedState != state)
                return false;

            // We are storing the code to later be able to refresh the access token
            VaultManager.Save(VaultTokenCodeResourceKey, VaultDefaultUnusedUserName, code);

            bool ok = await AcquireAccessTokenAsync(code, state).ConfigureAwait(false); ;
            return ok;
        }

        async Task<bool> AcquireAccessTokenAsync(string code, string state)
        {
            var contentParams = new Dictionary<string, string>
                                        {
                                        {"client_id", GhOAuthConfiguration.ClientId },
                                        {"client_secret", GhOAuthConfiguration.ClientSecret },
                                        {"code", code },
                                        {"state", state }
                                        };

            string content = String.Join("&", contentParams.Select(p => String.Format("{0}={1}", p.Key, p.Value)));

            try
            {
                SetLastError(null);

                var response = await _gitHubClient.Connection.Post<OAuthTokenResponse>(
                        new Uri("https://github.com/login/oauth/access_token", UriKind.Absolute),
                        content,
                        "application/json",
                        "application/x-www-form-urlencoded").ConfigureAwait(false);

                // TODO: save the entire json token (expiration time / renewal)
                string token = response.Body.access_token;

                var storedTokenInfo = new TokenSecurityInfo()
                {
                    AccessToken = token
                };

                VaultManager.Save(VaultTokenInfoResourceKey,
                    VaultDefaultUnusedUserName,
                    JsonConvert.SerializeObject(storedTokenInfo));

                return true;
            }
            catch (Exception ex)
            {
                SetLastError(ex);
            }

            return false;
        }

        public async Task<Credentials> GetCredentialsAsync()
        {
            string tokenInfoJson = VaultManager.GetPassword(VaultTokenInfoResourceKey, VaultDefaultUnusedUserName);

            if (String.IsNullOrWhiteSpace(tokenInfoJson))
            {
                // There has to be at least a JSON somewhere (even if invalid at this time) - otherwise the authz page was somehow not called
                throw new InvalidOperationException();   
            }

            var tokenInfo = JsonConvert.DeserializeObject<TokenSecurityInfo>(tokenInfoJson);

            // TODO: do we need to refresh the access token?

            return new Credentials(tokenInfo.AccessToken);
        }


        // TODO: Make this more intelligent - currently no token == not authorized (but it could be only expired)
        public static bool IsAppAuthorized()
        {
            return VaultManager.HasCredentials(VaultTokenInfoResourceKey);
        }
    }
}
