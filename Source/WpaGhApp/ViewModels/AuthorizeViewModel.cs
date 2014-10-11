using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Caliburn.Micro;
using WpaGhApp.Services;
using WpaGhApp.ViewModels.Main;

namespace WpaGhApp.ViewModels
{
    public class AuthorizeViewModel : Screen, IAuthorizeViewModelBindings
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly INavigationService _navigationService;

        public AuthorizeViewModel(IResourceLoader loader, IGitHubService githubService, INavigationService navigationService)
        {
            _loader = loader;
            _githubService = githubService;
            _navigationService = navigationService;

            InfoMessage = _loader.GetString("AuthZ_WhatIsOAuthDoing");
            ShowAuthorizeButton = true;
        }

        public string ApplicationName
        {
            get { return _loader.GetString(WellknownStringResources.ApplicationTitle); }
        }

        public bool Working { get; set; }
        public string InfoMessage { get; set; }
        public bool ShowAuthorizeButton { get; set; }

        public void AuthorizeApp()
        {
            _githubService.StartOAuthFlow();
        }

        public async Task ContinueAuthorizationAsync(WebAuthenticationResult result)
        {
            if (result.ResponseStatus != WebAuthenticationStatus.Success)
            {
                InfoMessage = _loader.GetString("AuthZ_OAuthFailed");
                return;
            }

            try
            {
                ShowAuthorizeButton = false;
                Working = true;
                InfoMessage = _loader.GetString("AuthZ_CompletingOAuthFlow_AcquiringAccessToken");

                bool ok = await _githubService.CompleteOAuthFlowAsync(result.ResponseData);

                if (ok)
                {
                    _navigationService
                        .UriFor<MainViewModel>()
                        .Navigate();
                }
                else
                {
                    InfoMessage = _loader.GetString("AuthZ_ObtainingAccessTokenFailed");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                ShowAuthorizeButton = true;
                Working = false;
            }
        }
    }
}
