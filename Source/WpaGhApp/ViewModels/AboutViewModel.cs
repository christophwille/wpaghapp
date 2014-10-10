using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Caliburn.Micro;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels
{
    public class AboutViewModel : Screen, IAboutViewModelBindings
    {
        private readonly IResourceLoader _loader;

        public AboutViewModel(IResourceLoader loader)
        {
            _loader = loader;
            VersionText = GetAppVersion();

            GitHubUrl = new Uri("https://github.com/christophwille/wpaghapp/");
            ReleaseHistoryUrl = new Uri("https://github.com/christophwille/wpaghapp/wiki/Release-History");
            PrivacyPolicyUrl = new Uri("https://github.com/christophwille/wpaghapp/wiki/Privacy-Policy");
        }
        public string ApplicationName
        {
            get { return _loader.GetString(WellknownStringResources.ApplicationTitle); }
        }

        public string VersionText { get; set; }

        private string GetAppVersion()
        {
            PackageVersion version = Package.Current.Id.Version;
            return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
        }

        public Uri GitHubUrl { get; set; }
        public Uri ReleaseHistoryUrl { get; set; }
        public Uri PrivacyPolicyUrl { get; set; }

        public async void ReviewTheApp()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }
    }
}
