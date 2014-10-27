using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using Octokit;
using WpaGhApp.Services;
using WpaGhApp.ViewModels.Org;

namespace WpaGhApp.ViewModels.Main
{

    public class OrgsListViewModel : AccountViewModelBase
    {
        public OrgsListViewModel(IGitHubService githubService, IMessageService messageService,
            IResourceLoader loader, INavigationService navigationService)
            : base(githubService, messageService, loader, navigationService)
        {
            DisplayName = "organizations";
        }

        protected async override Task<IReadOnlyList<Account>> GetAccountsAsync()
        {
            return await _githubService.GetOrganizationsAsync(Login);
        }

        protected override void NavigateToAccount(Account account)
        {
            var org = account as Octokit.Organization;

            _navigationService.UriFor<OrgViewModel>()
                .WithParam(vm => vm.OrgJson, JsonConvert.SerializeObject(org))
                .Navigate();
        }
    }
}
