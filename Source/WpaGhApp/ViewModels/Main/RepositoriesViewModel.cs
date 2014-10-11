using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class RepositoriesViewModel: Screen, IRepositoriesViewModelBindings
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly IMessageService _messageService;

        public RepositoriesViewModel(IResourceLoader loader, IGitHubService githubService, IMessageService messageService)
        {
            _loader = loader;
            _githubService = githubService;
            _messageService = messageService;

            DisplayName = "repositories";
        }

        public ObservableCollection<Octokit.Repository> Repositories { get; private set; }
        protected async override void OnActivate()
        {
            var repos = await _githubService.GetRepositoriesAsync();

            if (null == repos)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Repositories = new ObservableCollection<Octokit.Repository>(repos);
            }
        }
    }
}
