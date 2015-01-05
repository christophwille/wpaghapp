using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.Models;

namespace WpaGhApp.ViewModels.Repository
{
    public interface IRepositoryIssuesViewModelBindings : IViewModelWithProgessIndicator
    {
        ObservableCollection<GhIssue> Issues { get; }
    }
}
