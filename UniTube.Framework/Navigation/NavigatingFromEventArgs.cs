using System;
using Windows.UI.Xaml.Navigation;

namespace UniTube.Framework.Navigation
{
    public sealed class NavigatingFromEventArgs : EventArgs
    {
        private readonly NavigatingCancelEventArgs _eventArgs;

        /// <summary>
        /// Gets or sets a value that indicates whether a pending navigation should be canceled.
        /// </summary>
        public bool Cancel
        {
            get => _eventArgs != null ? _eventArgs.Cancel : false;
            set
            {
                if (_eventArgs != null)
                    _eventArgs.Cancel = value;
            }
        }

        /// <summary>
        /// Gets the value of the mode parameter from the originating Navigate call.
        /// </summary>
        public NavigationMode NavigationMode { get; set; }

        /// <summary>
        /// Gets the navigation parameter associated with this navigation.
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// Gets the value of the SourcePageType parameter from the originating Navigate call.
        /// </summary>
        public Type SourcePageType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatingFromEventArgs"/> class.
        /// </summary>
        public NavigatingFromEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigatingFromEventArgs"/> class based on
        /// <see cref="NavigatingCancelEventArgs"/>.
        /// </summary>
        /// <param name="args"></param>
        public NavigatingFromEventArgs(NavigatingCancelEventArgs args)
        {
            _eventArgs = args;

            NavigationMode = args.NavigationMode;
            Parameter = args.Parameter;
            SourcePageType = args.SourcePageType;
        }
    }
}
