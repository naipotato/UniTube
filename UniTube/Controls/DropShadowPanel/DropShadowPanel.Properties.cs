using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;

namespace UniTube.Controls
{
    public partial class DropShadowPanel
    {
        /// <summary>
        /// Identifies the <see cref="BlurRadius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register(
            nameof(BlurRadius), typeof(double), typeof(DropShadowPanel), new PropertyMetadata(9.0, OnBlurRadiusChanged));

        /// <summary>
        /// Identifies the <see cref="Color"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            nameof(Color), typeof(Color), typeof(DropShadowPanel), new PropertyMetadata(Colors.Black, OnColorChanged));

        /// <summary>
        /// Identifies the <see cref="OffsetX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register(
            nameof(OffsetX), typeof(double), typeof(DropShadowPanel), new PropertyMetadata(0.0, OnOffsetXChanged));

        /// <summary>
        /// Identifies the <see cref="OffsetY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register(
            nameof(OffsetY), typeof(double), typeof(DropShadowPanel), new PropertyMetadata(0.0, OnOffsetYChanged));

        /// <summary>
        /// Identifies the <see cref="OffsetZ"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty OffsetZProperty = DependencyProperty.Register(
            nameof(OffsetZ), typeof(double), typeof(DropShadowPanel), new PropertyMetadata(0.0, OnOffsetZChanged));

        /// <summary>
        /// Identifies the <see cref="ShadowOpacity"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(
            nameof(ShadowOpacity), typeof(double), typeof(DropShadowPanel), new PropertyMetadata(1.0, OnShadowOpacityChanged));

        /// <summary>
        /// Gets the drop shadow used.
        /// </summary>
        public DropShadow DropShadow => _dropShadow;

        /// <summary>
        /// Gets of sets the mask of the drop shadow.
        /// </summary>
        public CompositionBrush Mask
        {
            get => _dropShadow?.Mask;
            set
            {
                if (_dropShadow != null)
                {
                    _dropShadow.Mask = value;
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the blur radius of the drop shadow.
        /// </summary>
        public double BlurRadius
        {
            get => (double)GetValue(BlurRadiusProperty);
            set => SetValue(BlurRadiusProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the color of the drop shadow.
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the offset on the X axis of the drop shadow.
        /// </summary>
        public double OffsetX
        {
            get => (double)GetValue(OffsetXProperty);
            set => SetValue(OffsetXProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the offset on the Y axis of the drop shadow.
        /// </summary>
        public double OffsetY
        {
            get => (double)GetValue(OffsetYProperty);
            set => SetValue(OffsetYProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the offset on the Z axis of the drop shadow.
        /// </summary>
        public double OffsetZ
        {
            get => (double)GetValue(OffsetZProperty);
            set => SetValue(OffsetZProperty, value);
        }
        
        /// <summary>
        /// Gets or sets the opacity of the drop shadow.
        /// </summary>
        public double ShadowOpacity
        {
            get => (double)GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        private static void OnBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var panel = d as DropShadowPanel;
            panel?.OnBlurRadiusChanged((double)e.NewValue);
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var panel = d as DropShadowPanel;
            panel?.OnColorChanged((Color)e.NewValue);
        }

        private static void OnOffsetXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var panel = d as DropShadowPanel;
            panel?.OnOffsetXChanged((double)e.NewValue);
        }

        private static void OnOffsetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var panel = d as DropShadowPanel;
            panel?.OnOffsetYChanged((double)e.NewValue);
        }

        private static void OnOffsetZChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var panel = d as DropShadowPanel;
            panel?.OnOffsetZChanged((double)e.NewValue);
        }

        private static void OnShadowOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue) return;

            var panel = d as DropShadowPanel;
            panel?.OnShadowOpacityChanged((double)e.NewValue);
        }
    }
}