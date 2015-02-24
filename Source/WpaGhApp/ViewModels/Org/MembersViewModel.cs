using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Octokit;
using WpaGhApp.Models;
using WpaGhApp.Services;
using WpaGhApp.ViewModels.Main;

namespace WpaGhApp.ViewModels.Org
{
    public class MembersViewModel : AccountViewModelBase
    {
        public MembersViewModel(IGitHubService githubService, IMessageService messageService,
            IResourceLoader loader, INavigationService navigationService)
            : base(githubService, messageService, loader, navigationService)
        {
            DisplayName = "members";
        }

        protected async override Task<IEnumerable<GhAccount>> GetAccountsAsync()
        {
            var orgMembers = await _githubService.GetOrganizationMembersAsync(Login);
            return GhUser.MapUsers(orgMembers);
        }
    }
}
