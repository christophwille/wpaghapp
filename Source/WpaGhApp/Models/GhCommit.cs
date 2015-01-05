using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhCommit
    {
        public string Message { get; set; }
        public DateTimeOffset AuthoringDate { get; set; }
        public string AuthorName { get; set; }
        public string HtmlUrl { get; set; }

        public static IEnumerable<GhCommit> ToModel(IReadOnlyList<Octokit.GitHubCommit> commits)
        {
            return commits.Select(c => new GhCommit()
            {
                Message = c.Commit.Message, 
                AuthorName = c.Commit.Author.Name, 
                AuthoringDate = c.Commit.Author.Date,
                HtmlUrl = c.HtmlUrl
            });
        }
    }
}
