using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Octokit;
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

        protected async override Task<IReadOnlyList<Account>> GetAccountsAsync()
        {
            return await _githubService.GetOrganizationMembersAsync(Login);
        }
    }
}
