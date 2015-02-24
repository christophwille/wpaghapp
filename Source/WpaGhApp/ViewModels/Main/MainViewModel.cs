using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;
using WpaGhApp.Models;
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
        private readonly OrgsListViewModel _vmOrgs;

        public MainViewModel(INavigationService navigationService, IResourceLoader loader,
            NewsViewModel vmNews, RepositoriesViewModel vmRepos, 
            FollowersViewModel vmFollowers, FollowingViewModel vmFollowing,
            OrgsListViewModel vmOrgs)
        {
            _navigationService = navigationService;
            _loader = loader;

            _vmNews = vmNews;
            _vmRepos = vmRepos;
            _vmFollowers = vmFollowers;
            _vmFollowing = vmFollowing;
            _vmOrgs = vmOrgs;

            PageTitle = "Your Account";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (null != User)
            {
                _vmRepos.UserLogin = User.Login;
                _vmFollowers.Login = User.Login;
                _vmFollowing.Login = User.Login;
                _vmOrgs.Login = User.Login;
            }

            // TODO: First find a way to get the private news items // Items.Add(_vmNews);
            Items.Add(_vmRepos);
            Items.Add(_vmFollowers);
            Items.Add(_vmFollowing);
            Items.Add(_vmOrgs);
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
                User = JsonConvert.DeserializeObject<GhUser>(_userJson);
                
                PageTitle = User.Login;
            }
        }

        public GhUser User { get; set; }

        public string PageTitle { get; set; }
    }
}
