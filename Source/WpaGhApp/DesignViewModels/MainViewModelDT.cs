using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.ViewModels.Main;

namespace WpaGhApp.DesignViewModels
{
    public class MainViewModelDT : IMainViewModelBindings
    {
        public MainViewModelDT()
        {
            ApplicationName = "OCTOCENTRAL";
            
        }

        public string ApplicationName { get; private set; }
    }
}
