using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using WpaGhApp.Common;
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

            DisplayName = "code";

            CurrentPath = "";
            Breadcrumbs = new ObservableCollection<GhTreeItem>();
        }

        public IGitHubRepositoryIdentifiers RepositoryId { get; set; }
        public bool Working { get; set; }

        protected async override void OnInitialize()
        {
            if (null == PathTree)
            {
                await LoadContentsAsync();
            }
        }

        public Dictionary<string, List<GhTreeItem>> PathTree { get; set; }
        public string CurrentPath { get; set; }
        public List<GhTreeItem> PathItems { get; set; }
        public ObservableCollection<GhTreeItem> Breadcrumbs { get; set; }

        private async Task LoadContentsAsync()
        {
            Working = true;

            var branches = await _githubService.GetBranchesAsync(RepositoryId);

            if (null == branches)
            {
                await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
            }
            else
            {
                // TODO: Make the branch selectable (and persist list in page state via model class GhBranch)
                var defaultBranch = branches.FirstOrDefault();
                string shaToPass = defaultBranch.Commit.Sha + "?recursive=1";
                var response = await _githubService.GetTreeAsync(RepositoryId, shaToPass);

                if (response == null)
                {
                    await _messageService.ShowAsync("An error occured. " + _githubService.LastErrorMessage);
                }
                else
                {
                    PathTree = TreeToPathDictionary(response.Tree);
                    SelectPath(new GhTreeItem() { Path = "", Name = "root" });
                }
            }

            Working = false;
        }

        private Dictionary<string, List<GhTreeItem>> TreeToPathDictionary(ICollection<Octokit.TreeItem> treeItems)
        {
            var dict = new Dictionary<string, List<GhTreeItem>>();

            foreach (var ti in treeItems)
            {
                int lastDirSeparator = ti.Path.LastIndexOf('/');
                string path = "";
                string name = ti.Path;
                if (-1 != lastDirSeparator)
                {
                    path = ti.Path.Substring(0, lastDirSeparator);
                    name = ti.Path.Substring(lastDirSeparator + 1);
                }

                if (!dict.ContainsKey(path))
                {
                    dict.Add(path, new List<GhTreeItem>());
                }

                var list = dict[path];
                list.Add(new GhTreeItem(ti, name, path));
            }

            return dict;
        }

        private void SelectPath(GhTreeItem pathTreeItem, bool insertInBackStack=true)
        {
            PathItems = null;
            var pathTree = this.PathTree;
            string path = pathTreeItem.Path;

            if (null != pathTree && null != path)
            {
                if (pathTree.ContainsKey(path))
                {
                    PathItems = pathTree[path];
                    CurrentPath = path;

                    if (insertInBackStack) Breadcrumbs.Add(pathTreeItem);
                }
            }
        }

        public void SelectContent(ItemClickEventArgs eventArgs)
        {
            var ti = eventArgs.ClickedItem as GhTreeItem;
            if (null == ti) return;

            // If it is a directory, open it
            if (ti.ItemType == Octokit.TreeType.Tree)
            {
                SelectPath(ti);
                return;
            }

            // TODO: File -> Web, SubDir -> reload with new path

            //_navigationService.UriFor<HtmlUrlViewModel>()
            //    .WithParam(vm => vm.PageTitle, issue.Title)
            //    .WithParam(vm => vm.HtmlUrl, issue.HtmlUrl)
            //    .Navigate();
        }

        public void NavigateBackstack(GhTreeItem ti)
        {
            for (var i = Breadcrumbs.Count() - 1; i >= 0; i--)
            {
                var item = Breadcrumbs[i];
                if (item != ti)   // TODO: Revisit this logic once we store page state
                {
                    Breadcrumbs.Remove(item);
                    continue;
                }
                break;
            }

            SelectPath(ti, insertInBackStack:false);
        }
    }
}
