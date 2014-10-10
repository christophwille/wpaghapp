using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.ViewModels
{
    public interface IAboutViewModelBindings : IViewModelWithApplicationName
    {
        string VersionText { get; set; }
        Uri GitHubUrl { get; set; }
        Uri PrivacyPolicyUrl { get; set; }
    }
}
