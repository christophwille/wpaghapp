using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using WpaGhApp.Common;
using WpaGhApp.Services;
using WpaGhApp.ViewModels.Main;

namespace WpaGhApp.ViewModels.Org
{
    public class OrgViewModel : Conductor<IScreen>.Collection.OneActive, IOrgViewModelBindings // , IStateEnabledViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _loader;
        private readonly MembersViewModel _vmMembers;
        private readonly RepositoriesViewModel _vmRepos;

        public OrgViewModel(INavigationService navigationService, IResourceLoader loader,
            MembersViewModel vmMembers, RepositoriesViewModel vmRepos)
        {
            _navigationService = navigationService;
            _loader = loader;

            _vmMembers = vmMembers;
            _vmRepos = vmRepos;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (null != Org)
            {
                _vmMembers.Login = Org.Login;
                _vmRepos.UserLogin = Org.Login;
            }

            Items.Add(_vmRepos);
            Items.Add(_vmMembers);
        }


        public string PageTitle { get; set; }

        private string _orgJson;
        public string OrgJson
        {
            get { return _orgJson; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                _orgJson = value;
                Org = JsonConvert.DeserializeObject<Octokit.Organization>(_orgJson);

                PageTitle = Org.Login;
            }
        }

        public Octokit.Organization Org { get; set; }
    }
}
