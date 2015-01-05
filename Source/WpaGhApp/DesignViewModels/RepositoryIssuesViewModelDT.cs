using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using WpaGhApp.Models;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryIssuesViewModelDT : IRepositoryIssuesViewModelBindings
    {
        public RepositoryIssuesViewModelDT()
        {
            Issues = new ObservableCollection<GhIssue>(new List<GhIssue>
            {
                new GhIssue()
                {
                    Title = "Get rid of all bugs in the app",
                    AssigneeLogin = "christophwille"
                },
                new GhIssue()
                {
                    Title = "We do not support multiline. It will simply be cut off at screen size",
                    AssigneeLogin = "dgrunwald"
                }
            });
        }
        public bool Working { get; set; }
        public ObservableCollection<GhIssue> Issues { get; private set; }
    }
}
