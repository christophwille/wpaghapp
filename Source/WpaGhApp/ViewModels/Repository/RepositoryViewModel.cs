using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Repository
{
    public class RepositoryViewModel : Conductor<IScreen>.Collection.OneActive, IRepositoryViewModelBindings
    {
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly RepositoryCommitsViewModel _vmCommits;
        private readonly RepositoryIssuesViewModel _vmIssues;
        private readonly RepositoryInfoViewModel _vmInfo;

        public RepositoryViewModel(INavigationService navigationService, IResourceLoader loader, IGitHubService githubService,
            RepositoryCommitsViewModel vmCommits, RepositoryIssuesViewModel vmIssues, RepositoryInfoViewModel vmInfo)
        {
            _navigationService = navigationService;
            _loader = loader;
            _githubService = githubService;
            _vmCommits = vmCommits;
            _vmIssues = vmIssues;
            _vmInfo = vmInfo;
        }

        private string _repositoryJson;
        public string RepositoryJson
        {
            get { return _repositoryJson; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                _repositoryJson = value;
                Repository = JsonConvert.DeserializeObject<Octokit.Repository>(_repositoryJson);
                RepositoryName = Repository.Name;
                RepositoryOwner = Repository.Owner.Login;
            }
        }

        public string RepositoryName { get; private set; }
        public string RepositoryOwner { get; private set; }

        public Octokit.Repository Repository { get; private set; }

        protected async override void OnInitialize()
        {
            base.OnInitialize();

            _vmInfo.Repository = this.Repository;
            _vmCommits.RepositoryId = this;
            _vmIssues.RepositoryId = this;

            Items.Add(_vmInfo);
            Items.Add(_vmCommits);
            Items.Add(_vmIssues);
        }
    }
}
