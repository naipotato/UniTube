namespace UniTube.Controls
{
    /// <summary>
    /// Defines constants that specify how the pane is shown in a NavigationView.
    /// </summary>
    public enum HamburgerMenuDisplayMode
    {
        /// <summary>
        /// The pane always shows as a narrow sliver which can be opened to full width.
        /// </summary>
        Compact = 0,

        /// <summary>
        /// The pane stays open alongside the content.
        /// </summary>
        Expanded = 1,

        /// <summary>
        /// Only the menu button remains fixed. The pane shows and hides as needed.
        /// </summary>
        Minimal = 2
    }
}
