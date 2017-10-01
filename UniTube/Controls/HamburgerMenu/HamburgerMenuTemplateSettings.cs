using Windows.UI.Xaml;

namespace UniTube.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class HamburgerMenuTemplateSettings : DependencyObject
    {
        /// <summary>
        /// 
        /// </summary>
        public GridLength CompactPaneLength { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public GridLength OpenPaneGridLength { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double OpenPaneLength { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public double OpenPaneLengthMinusCompactLength { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HamburgerMenuTemplateSettings"/> class.
        /// </summary>
        internal HamburgerMenuTemplateSettings(double compactPaneLength, double openPaneLength)
        {
            CompactPaneLength = new GridLength(compactPaneLength);
            OpenPaneGridLength = new GridLength(openPaneLength);
            OpenPaneLength = openPaneLength;
            OpenPaneLengthMinusCompactLength = (openPaneLength - compactPaneLength);
        }
    }
}
