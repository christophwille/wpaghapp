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
    }
}
