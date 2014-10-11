using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace WpaGhApp.ViewModels
{
    public class HtmlUrlViewModel : Screen, IHtmlUrlViewModel
    {
        public string PageTitle { get; set; }
        public string HtmlUrl { get; set; }
    }
}
