using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhContent
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string HtmlUrl { get; set; }

        public static IEnumerable<GhContent> ToModel(IReadOnlyList<Octokit.RepositoryContent> contents)
        {
            return contents.Select(c => new GhContent()
            {
                Name = c.Name,
                Path = c.Path,
                HtmlUrl = c.HtmlUrl != null ? c.HtmlUrl.ToString() : null
            });
        }
    }
}
