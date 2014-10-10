using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.ViewModels;

namespace WpaGhApp.DesignViewModels
{
    public class AboutViewModelDT : IAboutViewModelBindings
    {
        public AboutViewModelDT()
        {
            ApplicationName = "OCTOCENTRAL";
            VersionText = "1.0";

            GitHubUrl = new Uri("http://chrison.net/");
            ReleaseHistoryUrl = new Uri("http://chrison.net/");
            PrivacyPolicyUrl = new Uri("http://chrison.net/");
        }

        public string ApplicationName { get; private set; }
        public string VersionText { get; set; }
        public Uri GitHubUrl { get; set; }
        public Uri ReleaseHistoryUrl { get; set; }
        public Uri PrivacyPolicyUrl { get; set; }
    }
}
