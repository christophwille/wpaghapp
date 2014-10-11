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
    public class CompositeRepositoryViewDT : IRepositoryIssuesViewModelBindings, IRepositoryCommitsViewModelBindings, IRepositoryViewModelBindings
    {
        public CompositeRepositoryViewDT()
        {
            RepositoryName = "winrt-snippets";
            RepositoryOwner = "christophwille";

            Commits = new ObservableCollection<GitHubCommit>(new List<GitHubCommit>
            {
                new GitHubCommit()
                {
                    Commit = new Commit()
                    {
                        Message = "Initial commit",
                        Author = new Signature()
                        {
                            Date = DateTime.Now,
                            Name = "christophwille"
                        }
                    }
                }
            });
        }

        public ObservableCollection<Octokit.GitHubCommit> Commits { get; private set; }
        public ObservableCollection<Octokit.Issue> Issues { get; private set; }

        public string RepositoryName { get; private set; }
        public string RepositoryOwner { get; private set; }
    }
}
