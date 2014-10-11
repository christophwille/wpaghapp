using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.Models;

namespace WpaGhApp.ViewModels
{
    public interface IViewModelWithRepositoryId
    {
        IGitHubRepositoryIdentifiers RepositoryId { get; set; }
    }
}
