using WpaGhApp.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WpaGhApp.Views
{
    public sealed partial class HtmlUrlView : Page
    {
        public HtmlUrlView()
        {
            this.InitializeComponent();
        }

        private void HtmlContent_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void HtmlContent_OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            WebViewProgressRing.IsActive = false;
            WebViewProgressRing.Visibility = Visibility.Collapsed;
            HtmlContent.Visibility = Visibility.Visible;
        }
    }
}
