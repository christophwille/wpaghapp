using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Octokit;
using WpaGhApp.Models;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class FollowingViewModel: AccountViewModelBase
    {
        public FollowingViewModel(IGitHubService githubService, IMessageService messageService, 
            IResourceLoader loader, INavigationService navigationService) : base(githubService, messageService, loader,navigationService)
        {
            DisplayName = "following";
        }

        protected async override Task<IEnumerable<GhAccount>> GetAccountsAsync()
        {
            var following = await _githubService.GetFollowingAsync(Login);
            return GhUser.MapUsers(following);
        }
    }
}
