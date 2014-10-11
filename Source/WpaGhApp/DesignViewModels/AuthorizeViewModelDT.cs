using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.ViewModels;

namespace WpaGhApp.DesignViewModels
{
    public class AuthorizeViewModelDT : IAuthorizeViewModelBindings
    {
        public AuthorizeViewModelDT()
        {
            ApplicationName = "OCTOCENTRAL";
            InfoMessage = "Stand in for design time of the info message on pre-authZ and access token acquiring";
            ShowAuthorizeButton = true;
        }

        public string ApplicationName { get; private set; }
        public bool Working { get; set; }
        public string InfoMessage { get; set; }
        public bool ShowAuthorizeButton { get; set; }
    }
}
