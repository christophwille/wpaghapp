using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.Common;
using WpaGhApp.Models;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryContentsViewModelDT : IRepositoryContentsViewModelBindings
    {
        public RepositoryContentsViewModelDT()
        {
            PathItems = new List<GhTreeItem>()
            {
                new GhTreeItem()
                {
                    Name = ".gitignore",
                    RelativePath = "",
                    Size = 45,
                    ItemType = Octokit.TreeType.Blob
                },
                new GhTreeItem()
                {
                    Name = "Source",
                    RelativePath = "",
                    Size = 0,
                    ItemType = Octokit.TreeType.Tree
                },
            };

            Breadcrumbs = new ObservableCollection<GhTreeItem>(new List<GhTreeItem>()
            {
                new GhTreeItem()
                {
                    Name = "root",
                },
                new GhTreeItem()
                {
                    Name = "level 1",
                },
                new GhTreeItem()
                {
                    Name = "second level",
                },
                new GhTreeItem()
                {
                    Name = "level three",
                },
                new GhTreeItem()
                {
                    Name = "4th level",
                },
            });
        }

        public bool Working { get; set; }
        public Dictionary<string, List<GhTreeItem>> PathTree { get; set; }
        public List<GhTreeItem> PathItems { get; set; }
        public ObservableCollection<GhTreeItem> Breadcrumbs { get; set; }
        public List<GhBranch> Branches { get; set; }
        public GhBranch SelectedBranch { get; set; }
    }
}
