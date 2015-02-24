using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    // In Octokit, classes User and Organization derive from Account
    // Thus we do the same here with our model classes
    public class GhAccount
    {
        protected static void BasicMemberMapping(Octokit.Account source, GhAccount target)
        {
            target.Login = source.Login;
            target.Url = source.Url;
            target.AvatarUrl = source.AvatarUrl;
        }

        public string Login { get; set; }
        public string Url { get; set; }
        public string AvatarUrl { get; set; }
    }
}
