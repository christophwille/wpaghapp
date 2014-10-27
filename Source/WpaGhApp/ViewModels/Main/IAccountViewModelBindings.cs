using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace WpaGhApp.ViewModels.Main
{
    public interface IAccountViewModelBindings : IViewModelWithProgessIndicator
    {
        // User, Organization derive from Account
        ObservableCollection<Octokit.Account> Accounts { get; }
    }
}
