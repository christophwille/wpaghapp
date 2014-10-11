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
    public class RepositoryCommitsViewModelDT : IRepositoryCommitsViewModelBindings
    {
        public RepositoryCommitsViewModelDT()
        {
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
                },
                new GitHubCommit()
                {
                    Commit = new Commit()
                    {
                        Message = "a long commit with \r\n a wrap in it",
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
        public bool Working { get; set; }
    }
}
