using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Models
{
    public interface IGitHubRepositoryIdentifiers
    {
        string RepositoryName { get; }
        string RepositoryOwner { get;  }
    }
}
