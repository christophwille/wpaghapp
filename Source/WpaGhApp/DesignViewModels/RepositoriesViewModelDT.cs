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
    public class RepositoriesViewModelDT : IRepositoriesViewModelBindings
    {
        public RepositoriesViewModelDT()
        {
            Repositories = new ObservableCollection<Repository>(new List<Repository>
            {
                new Repository()
                {
                    Name = "wpaghapp",
                    Description = "Windows Phone Application for GitHub"
                },
                new Repository()
                {
                    Name = "viennarealtime",
                    Description = "City of Vienna public transport departure information"
                },
            });
        }

        public bool Working { get; set; }
        public ObservableCollection<Repository> Repositories { get; private set; }
    }
}
