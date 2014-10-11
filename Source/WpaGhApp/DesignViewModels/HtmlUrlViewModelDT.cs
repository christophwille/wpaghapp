using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpaGhApp.ViewModels;

namespace WpaGhApp.DesignViewModels
{
    public class HtmlUrlViewModelDT : IHtmlUrlViewModel
    {
        public HtmlUrlViewModelDT()
        {
            PageTitle = "A long title that might \r\n overflow anyways";
        }

        public string PageTitle { get; set; }
        public string HtmlUrl { get; set; }
    }
}
