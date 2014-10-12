using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IMainViewModelBindings
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

            PageTitle = "Your Account";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (null != User)
            {
                _vmRepos.UserLogin = User.Login;
                _vmFollowers.UserLogin = User.Login;
                _vmFollowing.UserLogin = User.Login;
            }

            // TODO: First find a way to get the private news items // Items.Add(_vmNews);
            Items.Add(_vmRepos);
            Items.Add(_vmFollowers);
            Items.Add(_vmFollowing);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void AboutApp()
        {
            _navigationService.UriFor<AboutViewModel>().Navigate();
        }

        public void ChangeAuthZ()
        {
            _navigationService.UriFor<AuthorizeViewModel>().Navigate();
        }

        private string _userJson;
        public string UserJson
        {
            get { return _userJson; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                _userJson = value;
                User = JsonConvert.DeserializeObject<Octokit.User>(_userJson);
                
                PageTitle = User.Login;
            }
        }

        public Octokit.User User { get; set; }

        public string PageTitle { get; set; }
    }
}
