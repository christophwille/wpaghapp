using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.ViewModels
{
    public interface IHtmlUrlViewModel
    {
        string PageTitle { get; set; }
        string HtmlUrl { get; set; }
    }
}
