using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Repository
{
    public class RepositoryViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly INavigationService _navigationService;
        private readonly IResourceLoader _loader;

        public RepositoryViewModel(INavigationService navigationService, IResourceLoader loader)
        {
            _navigationService = navigationService;
            _loader = loader;


        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            // Items.Add(_vmIssues);
        }
    }
}
