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
    public class FollowViewModelBase : Screen, IFollowViewModelBindings
    {
        protected readonly IResourceLoader _loader;
        protected readonly INavigationService _navigationService;
        protected readonly IGitHubService _githubService;
        protected readonly IMessageService _messageService;

        public FollowViewModelBase(IGitHubService githubService, IMessageService messageService, 
            IResourceLoader loader,
            INavigationService navigationService)
        {
            _githubService = githubService;
            _messageService = messageService;
            _loader = loader;
            _navigationService = navigationService;
        }

        public string UserLogin { get; set; }
        public bool Working { get; set; }
        public ObservableCollection<User> Users { get; private set; }

        protected override async void OnInitialize()
        {
            Working = true;
            var users = await GetFollowUsersAsync();
            Working = false;

            if (null == users)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Users = new ObservableCollection<User>(users);
            }
        }

        protected virtual Task<IReadOnlyList<User>> GetFollowUsersAsync()
        {
            throw new NotImplementedException();
        }

        public void SelectUser(ItemClickEventArgs eventArgs)
        {
            var user = eventArgs.ClickedItem as Octokit.User;
            if (null == user) return;

            _navigationService.UriFor<MainViewModel>()
                .WithParam(vm => vm.UserJson, JsonConvert.SerializeObject(user))
                .Navigate();
        }
    }
}
