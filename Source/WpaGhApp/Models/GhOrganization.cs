using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public class GhOrganization : GhAccount
    {
        public GhOrganization()
        {
        }

        public GhOrganization(Octokit.Organization org)
        {
            BasicMemberMapping(org, this);
        }

        public static GhOrganization MapOrganization(Octokit.Organization org)
        {
            return new GhOrganization(org);
        }

        public static IEnumerable<GhOrganization> MapOrganizations(IReadOnlyList<Octokit.Organization> orgs)
        {
            if (null == orgs) return null;
            return orgs.Select(GhOrganization.MapOrganization);
        }
    }
}
