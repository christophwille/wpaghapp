using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryInfoViewModelDT : IRepositoryInfoViewModelBindings
    {
        public RepositoryInfoViewModelDT()
        {
            Repository = new Octokit.Repository()
            {
                ForksCount = 0,
                OpenIssuesCount = 155,
                StargazersCount = 2,
                WatchersCount = 20
            };
        }

        public Octokit.Repository Repository { get; set; }
    }
}
