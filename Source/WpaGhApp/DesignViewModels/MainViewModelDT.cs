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
            PageTitle = "Your Account";
            
        }

        public string PageTitle { get; set; }
    }
}
