using System;

using Windows.UI.Xaml;

namespace UniTube.Controls
{
    public partial class Loading
    {
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive), typeof(bool), typeof(Loading), new PropertyMetadata(default(bool), OnIsActiveChanged));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var loading = d as Loading;
            loading.OnIsActiveChanged((bool)e.NewValue);
        }
    }
}
