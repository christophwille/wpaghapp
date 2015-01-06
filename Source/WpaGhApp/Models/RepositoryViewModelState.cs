using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class RepositoryViewModelState
    {
        public int ActiveItemIndex { get; set; }
        public List<GhCommit> Commits { get; set; }
        public List<GhIssue> Issues { get; set; }

        // State for repository content viewmodel
        public Dictionary<string, List<GhTreeItem>> PathTree { get; set; }
        public List<GhTreeItem> Breadcrumbs { get; set; }
        public List<GhBranch> Branches { get; set; }
        public string SelectedBranchName { get; set; }
    }
}
