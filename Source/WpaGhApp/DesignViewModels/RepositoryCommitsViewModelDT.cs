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
    public class RepositoryCommitsViewModelDT : IRepositoryCommitsViewModelBindings
    {
        public RepositoryCommitsViewModelDT()
        {
            Commits = new ObservableCollection<GhCommit>(new List<GhCommit>
            {
                new GhCommit()
                {
                 Message = "Initial commit",
                 AuthoringDate = DateTime.Now,
                 AuthorName = "christophwille"
                },
                new GhCommit()
                {
                 Message = "a long commit with \r\n a wrap in it",
                 AuthoringDate = DateTime.Now,
                 AuthorName = "christophwille"
                }
            });

        }

        public ObservableCollection<GhCommit> Commits { get; private set; }
        public bool Working { get; set; }
    }
}
