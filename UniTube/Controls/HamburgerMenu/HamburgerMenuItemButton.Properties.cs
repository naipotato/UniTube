using Windows.UI.Xaml;

namespace UniTube.Controls
{
    public partial class HamburgerMenuItemButton
    {
        public static readonly DependencyProperty CompactPaneLengthProperty = DependencyProperty.Register(
            nameof(CompactPaneLength), typeof(double), typeof(HamburgerMenuItemButton), new PropertyMetadata(0d));

        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(
            nameof(Glyph), typeof(string), typeof(HamburgerMenuItemButton), new PropertyMetadata(null));

        public double CompactPaneLength
        {
            get { return (double)GetValue(CompactPaneLengthProperty); }
            private set { SetValue(CompactPaneLengthProperty, value); }
        }

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }
    }
}
