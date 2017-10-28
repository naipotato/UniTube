using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace UniTube.Controls
{
    public partial class LoadingView
    {
        /// <summary>
        /// Identifies the <see cref="IsLoading"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsLoading), typeof(bool), typeof(LoadingView), new PropertyMetadata(default(bool), OnIsActiveChanged));

        public static readonly DependencyProperty LoadingBackgroundProperty = DependencyProperty.Register(
            nameof(LoadingBackground), typeof(Brush), typeof(LoadingView), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty RingForegroundProperty = DependencyProperty.Register(
            nameof(RingForeground), typeof(Brush), typeof(LoadingView), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Gets or sets a value that indicates whether content is loading.
        /// </summary>
        public bool IsLoading
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public Brush LoadingBackground
        {
            get => (Brush)GetValue(LoadingBackgroundProperty);
            set => SetValue(LoadingBackgroundProperty, value);
        }

        public Brush RingForeground
        {
            get => (Brush)GetValue(RingForegroundProperty);
            set => SetValue(RingForegroundProperty, value);
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var loading = d as LoadingView;
            loading.OnIsActiveChanged((bool)e.NewValue);
        }
    }
}
