using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.Services
{
    public class DefaultResourceLoader : IResourceLoader
    {
        private Windows.ApplicationModel.Resources.ResourceLoader _loader;
        public DefaultResourceLoader()
        {
            _loader = new Windows.ApplicationModel.Resources.ResourceLoader();
        }

        public string GetString(string resourceKey)
        {
            return _loader.GetString(resourceKey);
        }
    }

    public static class WellknownStringResources
    {
        public const string ApplicationTitle = "ApplicationTitle";
    }
}
