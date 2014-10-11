﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpaGhApp.ViewModels
{
    public interface IAuthorizeViewModelBindings : IViewModelWithApplicationName, IViewModelWithProgessIndicator
    {
        bool Working { get; set; }
        string InfoMessage { get; set; }
        bool ShowAuthorizeButton { get; set; }
    }
}
