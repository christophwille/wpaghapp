using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _loader;
        private readonly NewsViewModel _vmNews;
        private readonly RepositoriesViewModel _vmRepos;
        private readonly FollowersViewModel _vmFollowers;
        private readonly FollowingViewModel _vmFollowing;

        public MainViewModel(INavigationService navigationService, IResourceLoader loader,
            NewsViewModel vmNews, RepositoriesViewModel vmRepos, FollowersViewModel vmFollowers, FollowingViewModel vmFollowing)
        {
            _navigationService = navigationService;
            _loader = loader;

            _vmNews = vmNews;
            _vmRepos = vmRepos;
            _vmFollowers = vmFollowers;
            _vmFollowing = vmFollowing;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            // TODO: First find a way to get the private news items // Items.Add(_vmNews);
            Items.Add(_vmRepos);
            Items.Add(_vmFollowers);
            Items.Add(_vmFollowing);
        }

        public string ApplicationName
        {
            get { return _loader.GetString(WellknownStringResources.ApplicationTitle); }
        }

        public void AboutApp()
        {
            _navigationService.UriFor<AboutViewModel>()
                .Navigate();
        }
    }
}
