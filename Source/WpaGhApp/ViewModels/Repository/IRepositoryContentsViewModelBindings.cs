using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.Common;
using WpaGhApp.Models;

namespace WpaGhApp.ViewModels.Repository
{
    public interface IRepositoryContentsViewModelBindings : IViewModelWithProgessIndicator
    {
        Dictionary<string, List<GhTreeItem>> PathTree { get; set; }
        List<GhTreeItem> PathItems { get; set; }
        ObservableCollection<GhTreeItem> Breadcrumbs { get; set; }
        List<GhBranch> Branches { get; set; }
        GhBranch SelectedBranch { get; set; }
    }
}
