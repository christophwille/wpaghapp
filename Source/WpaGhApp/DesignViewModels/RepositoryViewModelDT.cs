using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryViewModelDT : IRepositoryViewModelBindings
    {
        public RepositoryViewModelDT()
        {
            RepositoryName = "winrt-snippets";
            RepositoryOwner = "christophwille";
        }
        
        public string RepositoryName { get; private set; }
        public string RepositoryOwner { get; private set; }
    }
}
