using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Newtonsoft.Json;
using WpaGhApp.Services;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.ViewModels.Main
{
    public class RepositoriesViewModel: Screen, IRepositoriesViewModelBindings
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;

        public RepositoriesViewModel(IResourceLoader loader, IGitHubService githubService, 
            IMessageService messageService, INavigationService navigationService)
        {
            _loader = loader;
            _githubService = githubService;
            _messageService = messageService;
            _navigationService = navigationService;

            DisplayName = "repositories";
        }

        public string UserLogin { get; set; }
        public bool Working { get; set; }
        public ObservableCollection<Octokit.Repository> Repositories { get; private set; }
        protected async override void OnInitialize()
        {
            Working = true;
            var repos = await _githubService.GetRepositoriesAsync(UserLogin);
            Working = false;if (null == repos)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Repositories = new ObservableCollection<Octokit.Repository>(repos);
            }
        }

        public void SelectRepository(ItemClickEventArgs eventArgs)
        {
            var repository = eventArgs.ClickedItem as Octokit.Repository;
            if (null == repository) return;
            
            _navigationService.UriFor<RepositoryViewModel>()
                .WithParam(vm => vm.RepositoryJson, JsonConvert.SerializeObject(repository))
                .Navigate();
        }
    }
}
