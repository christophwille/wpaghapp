using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using WpaGhApp.Models;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Repository
{
    public class RepositoryCommitsViewModel : Screen, IRepositoryCommitsViewModelBindings, IViewModelWithRepositoryId
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;

        public RepositoryCommitsViewModel(IResourceLoader loader, IGitHubService githubService,
            IMessageService messageService, INavigationService navigationService)
        {
            _loader = loader;
            _githubService = githubService;
            _messageService = messageService;
            _navigationService = navigationService;

            DisplayName = "commits";
        }

        public IGitHubRepositoryIdentifiers RepositoryId { get; set; }

        public ObservableCollection<Octokit.GitHubCommit> Commits { get; private set; }
        protected async override void OnActivate()
        {
            var commits = await _githubService.GetCommitsAsync(RepositoryId);

            if (null == commits)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Commits = new ObservableCollection<Octokit.GitHubCommit>(commits);
            }
        }

        public void SelectCommit(ItemClickEventArgs eventArgs)
        {
            var ghCommit = eventArgs.ClickedItem as Octokit.GitHubCommit;
            if (null == ghCommit) return;

            //_navigationService.UriFor<RepositoryViewModel>()
            //    .WithParam(vm => vm.RepositoryJson, JsonConvert.SerializeObject(repository))
            //    .Navigate();
        }
    }
}
