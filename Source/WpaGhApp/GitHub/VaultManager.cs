using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace WpaGhApp.Github
{
    // Based off of https://gist.github.com/LucasMoffitt/11018418
    public static class VaultManager
    {
        private static readonly PasswordVault Vault = new PasswordVault();

        public static string GetUserNameIfExists(string vaultKeyResource)
        {
            try
            {
                var credentials = Vault.FindAllByResource(vaultKeyResource).FirstOrDefault();
                return credentials.UserName;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetPassword(string vaultKeyResource, string username)
        {
            try
            {
                return Vault.Retrieve(vaultKeyResource, username).Password;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool Save(string vaultKeyResource, string userName, string password)
        {
            try
            {
                if (HasCredentials(vaultKeyResource))
                    Delete(vaultKeyResource, userName);

                Vault.Add(new PasswordCredential(vaultKeyResource, userName, password));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Delete(string vaultKeyResource, string userName)
        {
            try
            {
                Vault.Remove(Vault.Retrieve(vaultKeyResource, userName));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool HasCredentials(string vaultKeyResource)
        {
            try
            {
                return Vault.FindAllByResource(vaultKeyResource).FirstOrDefault() != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
