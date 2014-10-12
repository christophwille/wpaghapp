using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WpaGhApp.Common
{
    public static class ListViewVerticalScrollPositionExtensions
    {
        // Note: Commented CW because not in use!
        //public static readonly DependencyProperty VerticalScrollPositionProperty =
        //    DependencyProperty.RegisterAttached(
        //        "VerticalScrollPosition",
        //        typeof(double?),
        //        typeof(ListViewVerticalScrollPositionExtensions),
        //        new PropertyMetadata(0.0, OnVerticalScrollPositionChanged));

        //public static double? GetVerticalScrollPosition(DependencyObject d)
        //{
        //    return (double?)d.GetValue(VerticalScrollPositionProperty);
        //}

        //public static void SetVerticalScrollPosition(DependencyObject d, double? value)
        //{
        //    d.SetValue(VerticalScrollPositionProperty, value);
        //}

        //private static void OnVerticalScrollPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var scrollPosition = (double?)d.GetValue(VerticalScrollPositionProperty);

        //    if (scrollPosition.HasValue)
        //    {
        //        var listView = (ListView)d;
        //        var sv = FindVisualChild<ScrollViewer>(listView);
        //        sv.ScrollToVerticalOffset(scrollPosition.Value);
        //    }
        //}

        // From http://msdn.microsoft.com/en-us/library/bb613579.aspx
        public static TChildItem FindVisualChild<TChildItem>(DependencyObject obj) where TChildItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TChildItem)
                    return (TChildItem)child;
                else
                {
                    TChildItem childOfChild = FindVisualChild<TChildItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
