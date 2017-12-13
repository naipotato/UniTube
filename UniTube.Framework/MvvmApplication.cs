using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UniTube.Framework.AppModel;
using UniTube.Framework.Mvvm;
using UniTube.Framework.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace UniTube.Framework
{
    /// <summary>
    /// Base class that provides MVVM patterns to your UWP application.
    /// </summary>
    public abstract class MvvmApplication : Application
    {
        #region Private variables
        private bool _handledOnResume;
        private object _pageKeys; 
        #endregion

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

        protected bool RestoreNavigationStateOnResume { get; private set; }
        protected ISessionStateService SessionStateService { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Override this method with logic that will be performed when resuming after the application is initialized.
        /// For example, refreshing user credentials.
        /// <para>This method is called after the navigation state is restored, triggering the
        /// <see cref="ViewModelBase.OnNavigatedTo"/> event on the active view model.</para>
        /// <para>Note: This is called whenever the app is resuming from suspend and terminate, but not during
        /// a fresh launch and not when reactivating the app if it hasn't been suspended.</para>
        /// </summary>
        /// <param name="args">
        /// The <see cref="IActivatedEventArgs"/> instance containing the event data if the app is activated. <c>null</c>
        /// if the app is only suspended and resumed without reactivation.</param>
        /// <returns>An <see cref="IAsyncAction"/> that represents an asynchronous action.</returns>
        protected virtual IAsyncAction OnResumeAsync(IActivatedEventArgs args) => Task.CompletedTask.AsAsyncAction();

#pragma warning disable CS4014
        private void OnResuming(object sender, object e)
        {
            if (RestoreNavigationStateOnResume)
                NavigationService.RestoreSavedNavigation();

            _handledOnResume = true;
            OnResumeAsync(null);
        }
#pragma warning restore CS4014

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

        /// <summary>
        /// Get a key dictionary to link the different pages to an enumerable.
        /// </summary>
        /// <typeparam name="T">The enum that represents the keys.</typeparam>
        /// <returns>The key dictionary.</returns>
        public Dictionary<T, Type> PageKeys<T>() where T : struct, IConvertible
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (_pageKeys != null && _pageKeys is Dictionary<T, Type>)
                return _pageKeys as Dictionary<T, Type>;

            return (_pageKeys = new Dictionary<T, Type>()) as Dictionary<T, Type>;
        }
        #endregion
    }
}
