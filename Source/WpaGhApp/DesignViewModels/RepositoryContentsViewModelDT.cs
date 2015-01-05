using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.Models;
using WpaGhApp.ViewModels.Repository;

namespace WpaGhApp.DesignViewModels
{
    public class RepositoryContentsViewModelDT : IRepositoryContentsViewModelBindings
    {
        public RepositoryContentsViewModelDT()
        {
            Contents = new ObservableCollection<GhContent>(new List<GhContent>
            {
                new GhContent()
                {
                 Name = ".gitignore",
                 Path = ".gitignore",
                },
                new GhContent()
                {
                 Name = ".gitignore",
                 Path = ".gitignore",
                }
            });
        }

        public bool Working { get; set; }
        public ObservableCollection<GhContent> Contents { get; set; }
    }
}
