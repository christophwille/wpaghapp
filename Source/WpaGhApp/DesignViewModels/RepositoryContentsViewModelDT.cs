using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.Models;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryContentsViewModelDT : IRepositoryContentsViewModelBindings
    {
        public RepositoryContentsViewModelDT()
        {
            CurrentPath = "";

            PathItems = new List<GhTreeItem>()
            {
                new GhTreeItem()
                {
                    Name = ".gitignore",
                    RelativePath = "",
                    Size = 45,
                    ItemType = Octokit.TreeType.Blob
                }
            };
        }

        public bool Working { get; set; }
        public Dictionary<string, List<GhTreeItem>> PathTree { get; set; }
        public string CurrentPath { get; set; }
        public List<GhTreeItem> PathItems { get; set; } 
    }
}
