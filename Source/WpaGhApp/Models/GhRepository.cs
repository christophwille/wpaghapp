using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhRepository
    {
        public static GhRepository ToModel(Octokit.Repository repo)
        {
            return new GhRepository()
            {
                Name= repo.Name,
                Description = repo.Description,
                ForksCount = repo.ForksCount,
                OpenIssuesCount = repo.OpenIssuesCount,
                StargazersCount = repo.StargazersCount,
                OwnerLogin = repo.Owner.Login
            };
        }

        public static IEnumerable<GhRepository> ToModel(IReadOnlyList<Octokit.Repository> repos)
        {
            return repos.Select(GhRepository.ToModel);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int ForksCount { get; set; }
        public int OpenIssuesCount { get; set; }
        public int StargazersCount { get; set; }

        // https://github.com/octokit/octokit.net/issues/735
        // public int SubscribersCount { get; set; }

        // Composite properties
        public string OwnerLogin { get; set; }
    }
}
