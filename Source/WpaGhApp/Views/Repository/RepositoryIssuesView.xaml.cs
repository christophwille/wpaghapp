using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using WpaGhApp.Common;

namespace WpaGhApp.Views.Repository
{
    public sealed partial class RepositoryIssuesView : UserControl, IStateEnabledPageChildElement
    {
        public RepositoryIssuesView()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        // http://blogs.msdn.com/b/priozersk/archive/2012/09/09/how-to-restore-scroll-position-of-the-gridview-when-navigating-back.aspx
        private ScrollViewer _scrollViewerIssuesListView;

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _scrollViewerIssuesListView =
                ListViewVerticalScrollPositionExtensions.FindVisualChild<ScrollViewer>(IssuesListView);
            StateEnabledPage.FindAndRegister(this);
        }

        private const string PageStateKey = "IssuesScrollViewerStateKey";

        public void LoadState(Dictionary<string, object> pageState)
        {
            if (pageState.ContainsKey(PageStateKey))
            {
                var verticalOffset = (double) pageState[PageStateKey];
                _scrollViewerIssuesListView.ScrollToVerticalOffset(verticalOffset);
            }
        }

        public void SaveState(Dictionary<string, object> pageState)
        {
            pageState[PageStateKey] = _scrollViewerIssuesListView.VerticalOffset;
        }
    }
}
