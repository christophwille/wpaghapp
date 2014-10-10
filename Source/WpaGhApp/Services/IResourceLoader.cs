using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Services
{
    public interface IResourceLoader
    {
        string GetString(string resourceKey);
    }
}
