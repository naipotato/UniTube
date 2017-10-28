using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Helpers
{
    public static class ListViewBehavior
    {
        public static readonly DependencyProperty MinItemWidthProperty = DependencyProperty.RegisterAttached(
            "MinItemWidth", typeof(double), typeof(ListViewBehavior), new PropertyMetadata(0, OnMinItemWidthChanged));

        public static readonly DependencyProperty FillBeforeWrapProperty = DependencyProperty.RegisterAttached(
            "FillBeforeWrap", typeof(bool), typeof(ListViewBehavior), new PropertyMetadata(false));

        public static double GetMinItemWidth(DependencyObject obj) => (double)obj.GetValue(MinItemWidthProperty);
        public static void SetMinItemWidth(DependencyObject obj, double value) => obj.SetValue(MinItemWidthProperty, value);

        public static bool GetFillBeforeWrap(DependencyObject obj) => (bool)obj.GetValue(FillBeforeWrapProperty);
        public static void SetFillBeforeWrap(DependencyObject obj, bool value) => obj.SetValue(FillBeforeWrapProperty, value);

        private static void OnMinItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ListViewBase)
            {
                (d as ListViewBase).SizeChanged -= OnListViewBaseSizeChanged;

                if ((double)e.NewValue > 0)
                    (d as ListViewBase).SizeChanged += OnListViewBaseSizeChanged;
            }
        }

        private static void OnListViewBaseSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var itemsControl = sender as ListViewBase;

            if (itemsControl.ItemsPanelRoot is ItemsWrapGrid itemsPanel)
            {
                var total = e.NewSize.Width - (10 + itemsControl.Padding.Right + itemsControl.Padding.Left);

                var itemMinSize = GetMinItemWidth(itemsControl);

                var canBeFit = Math.Floor(total / itemMinSize);

                if (GetFillBeforeWrap(itemsControl) && itemsControl.Items.Count > 0 && itemsControl.Items.Count < canBeFit)
                    canBeFit = itemsControl.Items.Count;

                itemsPanel.ItemWidth = total / canBeFit;
            }
        }
    }
}
