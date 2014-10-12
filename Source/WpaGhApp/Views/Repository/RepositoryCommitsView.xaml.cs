using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WpaGhApp.Common;

namespace WpaGhApp.Views.Repository
{
    public sealed partial class RepositoryCommitsView : UserControl, IStateEnabledPageChildElement
    {
        public RepositoryCommitsView()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        // http://blogs.msdn.com/b/priozersk/archive/2012/09/09/how-to-restore-scroll-position-of-the-gridview-when-navigating-back.aspx
        private ScrollViewer _scrollViewerCommitsListView;
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _scrollViewerCommitsListView = ListViewVerticalScrollPositionExtensions.FindVisualChild<ScrollViewer>(CommitsListView);
            
            var stateEnabledParentPage = StateEnabledPage.FindAsParent(this);
            if (null != stateEnabledParentPage)
            {
                stateEnabledParentPage.RegisterForSaveStateAndImmediateLoad(this);
            }
        }

        private double _verticalOffset;
        private const string PageStateKey = "CommitsScrollViewerStateKey";
        public void LoadState(Dictionary<string, object> pageState)
        {
            if (pageState.ContainsKey(PageStateKey))
            {
                _verticalOffset = (double)pageState[PageStateKey];
                _scrollViewerCommitsListView.ScrollToVerticalOffset(_verticalOffset);
            }
        }

        public void SaveState(Dictionary<string, object> pageState)
        {
            pageState[PageStateKey] = _scrollViewerCommitsListView.VerticalOffset;
        }
    }
}
