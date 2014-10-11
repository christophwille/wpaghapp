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
    public class RepositoryIssuesViewModel : Screen, IRepositoryIssuesViewModelBindings, IViewModelWithRepositoryId
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;

        public RepositoryIssuesViewModel(IResourceLoader loader, IGitHubService githubService,
            IMessageService messageService, INavigationService navigationService)
        {
            _loader = loader;
            _githubService = githubService;
            _messageService = messageService;
            _navigationService = navigationService;

            DisplayName = "issues";
        }

        public IGitHubRepositoryIdentifiers RepositoryId { get; set; }
        public bool Working { get; set; }
        public ObservableCollection<Octokit.Issue> Issues { get; private set; }
        protected async override void OnInitialize()
        {
            Working = true;
            var issues = await _githubService.GetIssuesAsync(RepositoryId);
            Working = false;

            if (null == issues)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                Issues = new ObservableCollection<Octokit.Issue>(issues);
            }
        }

        public void SelectIssue(ItemClickEventArgs eventArgs)
        {
            var issue = eventArgs.ClickedItem as Octokit.Issue;
            if (null == issue) return;

            _navigationService.UriFor<HtmlUrlViewModel>()
                .WithParam(vm => vm.PageTitle, issue.Title)
                .WithParam(vm => vm.HtmlUrl, issue.HtmlUrl.ToString()) // this is Uri, with commit it is string....
                .Navigate();
        }
    }
}
