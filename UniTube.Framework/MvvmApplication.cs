using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI.Xaml;

namespace UniTube.Framework
{
    /// <summary>
    /// Base class that provides MVVM patterns to your UWP application.
    /// </summary>
    public abstract class MvvmApplication : Application
    {
        private object _pageKeys;

        /// <summary>
        /// Initialize a new instance of the <see cref="MvvmApplication"/> class.
        /// </summary>
        public MvvmApplication()
        {
            Current = this;

            Suspending += OnSuspending;
            Resuming += OnResuming;
        }

        #region Properties
        /// <summary>
        /// Gets the actual instance of the <see cref="MvvmApplication"/> class.
        /// </summary>
        public new static MvvmApplication Current { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuspending { get; private set; }

        protected INavigationService NavigationService { get; private set; }

        protected ISessionStateService SessionStateService { get; private set; }
        #endregion
        private void OnResuming(object sender, object e)
        {
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            IsSuspending = true;
            try
            {
                var deferral = e.SuspendingOperation.GetDeferral();

                await OnSuspendingAsync(sender, e);

                NavigationService.Suspending();

                await SessionStateService.SaveAsync();

                deferral.Complete();
            }
            finally
            {
                IsSuspending = false;
            }
        }

        /// <summary>
        /// Invoked when the application is suspending, but before the general suspension calls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>An <see cref="IAsyncAction"/> that represents an asynchronous action.</returns>
        protected virtual IAsyncAction OnSuspendingAsync(object sender, SuspendingEventArgs e)
            => Task.CompletedTask.AsAsyncAction();

        public Dictionary<T, Type> PageKeys<T>() where T : struct, IConvertible
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (_pageKeys != null && _pageKeys is Dictionary<T, Type>)
                return _pageKeys as Dictionary<T, Type>;

            return (_pageKeys = new Dictionary<T, Type>()) as Dictionary<T, Type>;
        }
    }
}
