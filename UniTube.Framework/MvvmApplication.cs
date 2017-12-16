using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using UniTube.Framework.AppModel;
using UniTube.Framework.Mvvm;
using UniTube.Framework.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UniTube.Framework
{
    /// <summary>
    /// Base class that provides MVVM patterns to your UWP application.
    /// </summary>
    public abstract class MvvmApplication : Application
    {
        #region Private variables
        private bool _handledOnResume;
        private bool _isRestoringFromTermination;
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

            RestoreNavigationStateOnResume = true;
        }

        #region Properties
        /// <summary>
        /// Gets the actual instance of the <see cref="MvvmApplication"/> class.
        /// </summary>
        public new static MvvmApplication Current { get; private set; }

        /// <summary>
        /// Gets the device gesture service.
        /// </summary>
        protected IDeviceGestureService DeviceGestureService { get; private set; }

        /// <summary>
        /// Factory for creating the ExtendedSplashScreen instance.
        /// </summary>
        protected Func<SplashScreen, UIElement> ExtendedSplashScreenFactory { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the application is suspending.
        /// </summary>
        public bool IsSuspending { get; private set; }

        /// <summary>
        /// Gets the navigation service
        /// </summary>
        protected INavigationService NavigationService { get; private set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the application triggers navigation restore on app resume.
        /// </summary>
        protected bool RestoreNavigationStateOnResume { get; set; }

        /// <summary>
        /// Gets the session state service.
        /// </summary>
        protected ISessionStateService SessionStateService { get; private set; }

        /// <summary>
        /// Gets the shell user interface.
        /// </summary>
        protected UIElement Shell { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Configures the <see cref="ViewModelLocator"/> used by this framework.
        /// </summary>
        protected virtual void ConfigureViewModelLocator()
            => ViewModelLocationProvider.SetDefaultViewModelFactory(Resolve);

        /// <summary>
        /// Creates and configures the container if using a container.
        /// </summary>
        protected virtual void CreateAndConfigureContainer() { }

        private IDeviceGestureService CreateDeviceGestureService()
        {
            var deviceGestureService = OnCreateDeviceGestureService();
            if (deviceGestureService == null)
            {
                deviceGestureService = new DeviceGestureService
                {
                    UseTitleBarBackButton = true
                };
            }

            return deviceGestureService;
        }

        private INavigationService CreateNavigationService(FrameFacade rootFrame, ISessionStateService sessionStateService)
            => OnCreateNavigationService(rootFrame) ?? new NavigationService(rootFrame, SessionStateService, GetPageType);

        private Frame CreateRootFrame() => OnCreateRootFrame() ?? new Frame();

        private ISessionStateService CreateSessionStateService()
            => OnCreateSessionStateService() ?? new SessionStateService();

        /// <summary>
        /// Creates the shell of the app.
        /// </summary>
        /// <param name="rootFrame">The root frame of the app.</param>
        /// <returns>The shell of the app.</returns>
        protected virtual UIElement CreateShell(Frame rootFrame) => rootFrame;

        /// <summary>
        /// Gets the type of the page based on a page token.
        /// </summary>
        /// <param name="pageToken">The page token.</param>
        /// <returns>The type of the page which corresponds to the specified token.</returns>
        protected virtual Type GetPageType(string pageToken)
        {
            var assemblyQualifiedAppType = GetType().AssemblyQualifiedName;

            var pageNameWithParameter = assemblyQualifiedAppType.Replace(GetType().FullName, GetType().Namespace + ".Views.{0}Page");

            var viewFullName = string.Format(CultureInfo.InvariantCulture, pageNameWithParameter, pageToken);
            var viewType = Type.GetType(viewFullName);

            if (viewType == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The page name {0} does not have an associated type in namespace {1}", pageToken, GetType().Namespace + ".Views"), nameof(pageToken));
            }

            return viewType;
        }

        /// <summary>
        /// Initializes the <see cref="Frame"/> and its content.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>An <see cref="IAsyncOperation{TResult}"/> of a <see cref="Frame"/> that holds the app
        /// content.</returns>
        protected virtual IAsyncOperation<Frame> InitializeFrameAsync(IActivatedEventArgs args) => Task.Run(async () =>
        {
            CreateAndConfigureContainer();

            var rootFrame = CreateRootFrame();

            if (ExtendedSplashScreenFactory != null)
            {
                var extendedSplashScreen = ExtendedSplashScreenFactory.Invoke(args.SplashScreen);
                rootFrame.Content = extendedSplashScreen;
            }

            var frameFacade = new FrameFacade(rootFrame);

            SessionStateService = CreateSessionStateService();

            SessionStateAwarePage.GetSessionStateForFrame = frame => SessionStateService.GetSessionStateForFrame(frameFacade);

            SessionStateService.RegisterFrame(frameFacade, "AppFrame");

            NavigationService = CreateNavigationService(frameFacade, SessionStateService);

            DeviceGestureService = CreateDeviceGestureService();
            DeviceGestureService.GoBackRequested += OnDeviceGestureServiceGoBackRequested;
            DeviceGestureService.GoForwardRequested += OnDeviceGestureServiceGoForwardRequested;

            ConfigureViewModelLocator();

            OnRegisterKnownTypesForSerialization();
            var canRestore = await SessionStateService.CanRestoreSessionStateAsync();
            var shouldRestore = canRestore && ShouldRestoreState(args);
            if (shouldRestore)
            {
                await SessionStateService.RestoreSessionStateAsync();
            }

            await OnInitializeAsync(args);

            if (shouldRestore)
            {
                try
                {
                    SessionStateService.RestoreFrameState();
                    NavigationService.RestoreSavedNavigation();
                    _isRestoringFromTermination = true;
                }
                catch { }
            }

            return rootFrame;
        }).AsAsyncOperation();

        private IAsyncAction InitializeShellAsync(IActivatedEventArgs args) => Task.Run(async () =>
        {
            if (Window.Current.Content == null)
            {
                var rootFrame = await InitializeFrameAsync(args);

                Shell = CreateShell(rootFrame);

                Window.Current.Content = Shell ?? rootFrame;
            }
        }).AsAsyncAction();

        /// <summary>
        /// Override this method with logic that will be performed after application is activated through other means
        /// than a normal launch (i.e. Voice Commands, URI activation, being used as a share target from another app).
        /// For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        protected virtual IAsyncAction OnActivateAsync(IActivatedEventArgs args) => Task.CompletedTask.AsAsyncAction();

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await InitializeShellAsync(args);

            if (Window.Current.Content != null && (!_isRestoringFromTermination || args != null))
            {
                await OnActivateAsync(args);
            }
            else if (Window.Current.Content != null && _isRestoringFromTermination && !_handledOnResume)
            {
                await OnResumeAsync(args);
            }

            Window.Current.Activate();
        }

        protected override void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args) => OnActivated(args);

        /// <summary>
        /// Creates the device gesture service. Use this to inject your own <see cref="IDeviceGestureService"/>
        /// implementation.
        /// </summary>
        /// <returns>The initialized device gesture service.</returns>
        protected virtual IDeviceGestureService OnCreateDeviceGestureService() => null;

        /// <summary>
        /// Creates the navigation service. Use this to inject your own <see cref="INavigationService"/>
        /// implementation.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        /// <returns>The initialized navigation service.</returns>
        protected virtual NavigationService OnCreateNavigationService(FrameFacade rootFrame) => null;

        /// <summary>
        /// Creates the root frame.
        /// </summary>
        /// <returns>The initialized root frame.</returns>
        protected virtual Frame OnCreateRootFrame() => null;

        /// <summary>
        /// Creates the session state service. Use this to inject youw own <see cref="ISessionStateService"/>
        /// implementation.
        /// </summary>
        /// <returns>The initialized session state service.</returns>
        protected virtual ISessionStateService OnCreateSessionStateService() => null;

        private void OnDeviceGestureServiceGoBackRequested(object sender, DeviceGestureEventArgs e)
        {
            if (!e.Handled)
            {
                if (NavigationService.CanGoBack())
                {
                    NavigationService.GoBack();
                    e.Handled = true;
                }
            }
        }

        private void OnDeviceGestureServiceGoForwardRequested(object sender, DeviceGestureEventArgs e)
        {
            if (!e.Handled)
            {
                if (NavigationService.CanGoForward())
                {
                    NavigationService.GoForward();
                    e.Handled = true;
                }
            }
        }

        protected override void OnFileActivated(FileActivatedEventArgs args) => OnActivated(args);

        protected override void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args) => OnActivated(args);

        protected override void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args) => OnActivated(args);

        /// <summary>
        /// Override this method with the initialization logic of your application. Here you can initialize services,
        /// repositories, and so on.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>An <see cref="IAsyncAction"/> that represents an asynchronous action.</returns>
        protected virtual IAsyncAction OnInitializeAsync(IActivatedEventArgs args)
            => Task.CompletedTask.AsAsyncAction();

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            await InitializeShellAsync(args);

            var tileId = AppManifestHelper.GetApplicationId();

            if (Window.Current.Content != null && (!_isRestoringFromTermination || (args != null && args.TileId != tileId)))
            {
                await OnStartAsync(args);
            }
            else if (Window.Current.Content != null && _isRestoringFromTermination)
            {
                await OnResumeAsync(args);
            }

            Window.Current.Activate();
        }

        /// <summary>
        /// Used for setting up the list of known types for the <see cref="SessionStateService"/>, using the
        /// <see cref="SessionStateService.RegisterKnownType(Type)"/> method.
        /// </summary>
        protected virtual void OnRegisterKnownTypesForSerialization() { }

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

        protected override void OnSearchActivated(SearchActivatedEventArgs args) => OnActivated(args);

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args) => OnActivated(args);

        protected abstract IAsyncAction OnStartAsync(LaunchActivatedEventArgs args);

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

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual object Resolve(Type type) => Activator.CreateInstance(type);

        /// <summary>
        /// Override this method to provide custom logic that determines whether the app should restore state from a previous
        /// session. By default, the app will only restore state when <see cref="IActivatedEventArgs.PreviousExecutionState"/>
        /// is <see cref="ApplicationExecutionState.Terminated"/>
        /// <para>Note: restoring from state will prevent <see cref="OnStartAsync(LaunchActivatedEventArgs)"/> from getting
        /// called, as that is only called during a fresh launch. Restoring will trigger
        /// <see cref="OnResumeAsync(IActivatedEventArgs)"/> instead.</para>
        /// </summary>
        /// <param name="args"></param>
        /// <returns><c>true</c> if the app should restore state. <c>false</c> if the app should perform a fresh launch.</returns>
        protected virtual bool ShouldRestoreState(IActivatedEventArgs args)
            => args.PreviousExecutionState == ApplicationExecutionState.Terminated;
        #endregion
    }
}
