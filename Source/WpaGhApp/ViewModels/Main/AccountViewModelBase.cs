using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Newtonsoft.Json;
using Octokit;
using WpaGhApp.Models;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class AccountViewModelBase : Screen, IAccountViewModelBindings
    {
        protected readonly IResourceLoader _loader;
        protected readonly INavigationService _navigationService;
        protected readonly IGitHubService _githubService;
        protected readonly IMessageService _messageService;

        public AccountViewModelBase(IGitHubService githubService, IMessageService messageService, 
            IResourceLoader loader,
            INavigationService navigationService)
        {
            _githubService = githubService;
            _messageService = messageService;
            _loader = loader;
            _navigationService = navigationService;
        }

        public string Login { get; set; }
        public bool Working { get; set; }
        public ObservableCollection<GhAccount> Accounts { get; private set; }

        protected override async void OnInitialize()
        {
            Working = true;
            var accountsEnumerable = await GetAccountsAsync();
            Working = false;

            if (null == accountsEnumerable)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Accounts = new ObservableCollection<GhAccount>(accountsEnumerable);
            }
        }

        protected virtual Task<IEnumerable<GhAccount>> GetAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public void SelectAccount(ItemClickEventArgs eventArgs)
        {
            var account = eventArgs.ClickedItem as GhAccount;
            if (null == account) return;

            NavigateToAccount(account);
        }

        protected virtual void NavigateToAccount(GhAccount account)
        {
            if (account is GhUser)
            {
                NavigateToUser((GhUser)account);
            }
        }

        protected void NavigateToUser(GhUser user)
        {
            _navigationService.UriFor<MainViewModel>()
                .WithParam(vm => vm.UserJson, JsonConvert.SerializeObject(user))
                .Navigate();
        }
    }
}
