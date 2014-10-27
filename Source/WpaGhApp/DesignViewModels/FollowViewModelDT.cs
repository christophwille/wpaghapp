using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using WpaGhApp.ViewModels.Main;

namespace WpaGhApp.DesignViewModels
{
    public class FollowViewModelDT : IAccountViewModelBindings
    {
        public ObservableCollection<Account> Accounts { get; private set; }

        public FollowViewModelDT()
        {
            Accounts = new ObservableCollection<Account>(new List<User>
            {
                new User()
                {
                    Login = "DerAlbertCom",
                    Url = "",
                    AvatarUrl = "https://avatars.githubusercontent.com/u/136992?v=2"
                },
                new User()
                {
                    Login = "dgrunwald",
                    Url = "",
                    AvatarUrl = "https://avatars.githubusercontent.com/u/243140?v=2"
                },
            });
        }

        public bool Working { get; set; }
    }
}
