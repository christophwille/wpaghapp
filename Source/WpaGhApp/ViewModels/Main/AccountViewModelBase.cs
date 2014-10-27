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
        public ObservableCollection<Account> Accounts { get; private set; }

        protected override async void OnInitialize()
        {
            Working = true;
            var users = await GetAccountsAsync();
            Working = false;

            if (null == users)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Accounts = new ObservableCollection<Account>(users);
            }
        }

        protected virtual Task<IReadOnlyList<Account>> GetAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public void SelectAccount(ItemClickEventArgs eventArgs)
        {
            var account = eventArgs.ClickedItem as Octokit.Account;
            if (null == account) return;

            NavigateToAccount(account);
        }

        protected virtual void NavigateToAccount(Octokit.Account account)
        {
            if (account is Octokit.User)
            {
                NavigateToUser(account as Octokit.User);
            }
        }

        protected void NavigateToUser(Octokit.User user)
        {
            _navigationService.UriFor<MainViewModel>()
                .WithParam(vm => vm.UserJson, JsonConvert.SerializeObject(user))
                .Navigate();
        }
    }
}
