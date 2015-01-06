using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhBranch
    {
        public string Name { get; set; }
        public string Sha { get; set; }

        public static IEnumerable<GhBranch> ToModel(IReadOnlyList<Octokit.Branch> branches)
        {
            return branches.Select(c => new GhBranch()
            {
                Name = c.Name,
                Sha = c.Commit.Sha,
            });
        }
    }
}
