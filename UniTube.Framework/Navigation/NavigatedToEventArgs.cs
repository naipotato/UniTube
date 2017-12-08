using System;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Framework.Navigation
{
    public sealed class NavigatedToEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a value that indicates the direction of movement during navigation.
        /// </summary>
        public NavigationMode NavigationMode { get; set; }

        /// <summary>
        /// Gets a parameter object passed to the target page for the navigation.
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// Gets the data type of the source page.
        /// </summary>
        public Type SourcePageType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatedToEventArgs"/> class.
        /// </summary>
        public NavigatedToEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatedToEventArgs"/> class based on
        /// <see cref="NavigationEventArgs"/>.
        /// </summary>
        /// <param name="args">The frame's <see cref="NavigationEventArgs"/>.</param>
        public NavigatedToEventArgs(NavigationEventArgs args)
        {
            NavigationMode = args.NavigationMode;
            Parameter = args.Parameter;
            SourcePageType = args.SourcePageType;
        }
    }
}
