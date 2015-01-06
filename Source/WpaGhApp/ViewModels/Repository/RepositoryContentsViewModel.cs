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
    public class RepositoryContentsViewModel : Screen, IRepositoryContentsViewModelBindings, IViewModelWithRepositoryId
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;

        public RepositoryContentsViewModel(IResourceLoader loader, IGitHubService githubService,
            IMessageService messageService, INavigationService navigationService)
        {
            _loader = loader;
            _githubService = githubService;
            _messageService = messageService;
            _navigationService = navigationService;

            DisplayName = "contents";
        }

        public IGitHubRepositoryIdentifiers RepositoryId { get; set; }
        public bool Working { get; set; }
        public ObservableCollection<GhContent> Contents { get; set; }

        protected async override void OnInitialize()
        {
            if (null == Contents)
            {
                await LoadContentsAsync();
            }
        }

        async Task LoadContentsAsync()
        {
            Working = true;
            var contents = await _githubService.GetContentsAsync(RepositoryId, "/");

            //// Sha for last commit of repository https://github.com/christophwille/azure-snippets/commits/master
            //// https://developer.github.com/v3/git/trees/#get-a-tree-recursively
            // var response = await _githubService.GetTreeAsync(RepositoryId, "6525378e80deee6fc2d61b92a501112dc7259059?recursive=1");

            Working = false;

            if (null == contents)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Contents = new ObservableCollection<GhContent>(GhContent.ToModel(contents));
            }
        }

        public void SelectContent(ItemClickEventArgs eventArgs)
        {
            var issue = eventArgs.ClickedItem as GhContent;
            if (null == issue) return;

            // TODO: File -> Web, SubDir -> reload with new path

            //_navigationService.UriFor<HtmlUrlViewModel>()
            //    .WithParam(vm => vm.PageTitle, issue.Title)
            //    .WithParam(vm => vm.HtmlUrl, issue.HtmlUrl)
            //    .Navigate();
        }
    }
}
