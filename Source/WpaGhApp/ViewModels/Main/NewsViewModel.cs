using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using WpaGhApp.Services;

namespace WpaGhApp.ViewModels.Main
{
    public class NewsViewModel : Screen
    {
        private readonly IResourceLoader _loader;
        private readonly IGitHubService _githubService;

        public NewsViewModel(IResourceLoader loader, IGitHubService githubService)
        {
            _loader = loader;
            _githubService = githubService;

            DisplayName = "news";
        }

        protected async override void OnActivate()
        {
            // Currently not working: await _githubService.GetNewsAsync();
        }
    }
}
