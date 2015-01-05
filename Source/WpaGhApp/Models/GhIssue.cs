using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhIssue
    {
        public string Title { get; set; }
        public string AssigneeLogin { get; set; }
        public string HtmlUrl { get; set; }

        public static IEnumerable<GhIssue> ToModel(IReadOnlyList<Octokit.Issue> issues)
        {
            return issues.Select(c => new GhIssue()
            {
                Title = c.Title,
                AssigneeLogin = c.Assignee != null ? c.Assignee.Login : null,
                HtmlUrl = c.HtmlUrl != null ? c.HtmlUrl.ToString() : null // this is Uri, with commit it is string....
            });
        }
    }
}
