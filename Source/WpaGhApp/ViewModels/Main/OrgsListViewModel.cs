using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using Octokit;
using WpaGhApp.Models;
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

        protected async override Task<IEnumerable<GhAccount>> GetAccountsAsync()
        {
            var orgs = await _githubService.GetOrganizationsAsync(Login);
            return GhOrganization.MapOrganizations(orgs);
        }

        protected override void NavigateToAccount(GhAccount account)
        {
            var org = account as GhOrganization;

            _navigationService.UriFor<OrgViewModel>()
                .WithParam(vm => vm.OrgJson, JsonConvert.SerializeObject(org))
                .Navigate();
        }
    }
}
