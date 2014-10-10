using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Octokit;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class FollowingViewModel: FollowViewModelBase
    {
        public FollowingViewModel(IGitHubService githubService, IMessageService messageService, 
            IResourceLoader loader, INavigationService navigationService) : base(githubService, messageService, loader,navigationService)
        {
            DisplayName = "following";
        }

        protected async override Task<IReadOnlyList<User>> GetFollowUsersAsync()
        {
            return await _githubService.GetFollowingAsync();
        }
    }
}
