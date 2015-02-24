using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhUser : GhAccount
    {
        public GhUser()
        {
        }

        public GhUser(Octokit.User source)
        {
            BasicMemberMapping(source, this);
        }

        public static IEnumerable<GhUser> MapUsers(IReadOnlyList<Octokit.User> users)
        {
            if (null == users) return null;
            return users.Select(source => new GhUser(source));
        }
    }
}
