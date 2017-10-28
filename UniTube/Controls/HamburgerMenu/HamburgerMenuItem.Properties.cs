using Windows.UI.Xaml;

namespace UniTube.Controls
{
    public partial class HamburgerMenuItem
    {
        public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(
            nameof(CompactPaneLength), typeof(double), typeof(HamburgerMenuItem), new PropertyMetadata(0d));

        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(
            nameof(Glyph), typeof(string), typeof(HamburgerMenuItem), new PropertyMetadata(null));

        public double CompactPaneLength
        {
            get => (double)GetValue(CompactPaneLengthProperty);
            private set => SetValue(CompactPaneLengthProperty, value);
        }

        public string Glyph
        {
            get => (string)GetValue(GlyphProperty);
            set => SetValue(GlyphProperty, value);
        }
    }
}
