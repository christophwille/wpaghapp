using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using WpaGhApp.Common;
using WpaGhApp.Models;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Repository
{
    public class RepositoryViewModel : Conductor<IScreen>.Collection.OneActive, IRepositoryViewModelBindings, IStateEnabledViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly RepositoryCommitsViewModel _vmCommits;
        private readonly RepositoryIssuesViewModel _vmIssues;
        private readonly RepositoryInfoViewModel _vmInfo;
        private readonly RepositoryContentsViewModel _vmContents;

        public RepositoryViewModel(INavigationService navigationService, IResourceLoader loader, IGitHubService githubService,
            RepositoryCommitsViewModel vmCommits, RepositoryIssuesViewModel vmIssues, 
            RepositoryInfoViewModel vmInfo, RepositoryContentsViewModel vmContents)
        {
            _navigationService = navigationService;
            _loader = loader;
            _githubService = githubService;
            _vmCommits = vmCommits;
            _vmIssues = vmIssues;
            _vmInfo = vmInfo;
            _vmContents = vmContents;
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

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _vmInfo.Repository = this.Repository;

            _vmCommits.RepositoryId = this;
            _vmIssues.RepositoryId = this;
            _vmContents.RepositoryId = this;

            Items.Add(_vmInfo);
            Items.Add(_vmCommits);
            Items.Add(_vmIssues);
            Items.Add(_vmContents);
        }

        public void LoadState(string jsonState)
        {
            var state = JsonConvert.DeserializeObject<RepositoryViewModelState>(jsonState);
            if (null == state) return;

            if (null != state.Commits)
            {
                _vmCommits.Commits = new ObservableCollection<GhCommit>(state.Commits);
            }
            if (null != state.Issues)
            {
                _vmIssues.Issues = new ObservableCollection<GhIssue>(state.Issues);
            }

            // Do this last as this would call OnInitialize right away before restoring sub-state
            var activeItem = Items.ElementAtOrDefault(state.ActiveItemIndex);
            ActiveItem = activeItem;
        }

        public string SaveState()
        {
            var state = new RepositoryViewModelState()
            {
                ActiveItemIndex = Items.IndexOf(ActiveItem)
            };

            if (null != _vmCommits.Commits) state.Commits = _vmCommits.Commits.ToList();
            if (null != _vmIssues.Issues) state.Issues = _vmIssues.Issues.ToList();

            return JsonConvert.SerializeObject(state);
        }
    }
}
