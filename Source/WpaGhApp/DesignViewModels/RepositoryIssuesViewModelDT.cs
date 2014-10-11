using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryIssuesViewModelDT : IRepositoryIssuesViewModelBindings
    {
        public RepositoryIssuesViewModelDT()
        {
            Issues = new ObservableCollection<Issue>(new List<Issue>
            {
                new Issue()
                {
                    Title = "Get rid of all bugs in the app",
                    Assignee = new User()
                    {
                        Login = "christophwille"
                    },
                },
                new Issue()
                {
                    Title = "We do not support multiline. It will simply be cut off at screen size",
                    Assignee = new User()
                    {
                        Login = "dgrunwald"
                    },
                }
            });
        }
        public bool Working { get; set; }
        public ObservableCollection<Octokit.Issue> Issues { get; private set; }
    }
}
