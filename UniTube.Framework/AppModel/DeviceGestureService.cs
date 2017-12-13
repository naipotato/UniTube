using System;
using System.ComponentModel;
using Windows.Devices.Input;
using Windows.Foundation.Metadata;
using Windows.Phone.UI.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace UniTube.Framework.AppModel
{
    public class DeviceGestureService : IDeviceGestureService, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public DeviceGestureService()
        {
            IsHardwareBackButtonPresent = ApiInformation.IsEventPresent("Windows.Phone.UI.Input.HardwareButtons", "BackPressed");
            IsHardwareCameraButtonPresent = ApiInformation.IsEventPresent("Windows.Phone.UI.Input.HardwareButtons", "CameraPressed");

            IsKeyboardPresent = new KeyboardCapabilities().KeyboardPresent != 0;
            IsMousePresent = new MouseCapabilities().MousePresent != 0;
            IsTouchPresent = new TouchCapabilities().TouchPresent != 0;

#if WINDOWS_PHONE_APP
            if (IsHardwareBackButtonPresent)
                HardwareButtons.BackPressed += OnHardwareButtonsBackPressed; 
#endif

            if (IsHardwareCameraButtonPresent)
            {
                HardwareButtons.CameraHalfPressed += OnHardwareButtonsCameraHalfPressed;
                HardwareButtons.CameraPressed += OnHardwareButtonsCameraPressed;
                HardwareButtons.CameraReleased += OnHardwareButtonsCameraReleased;
            }

            if (IsMousePresent)
                MouseDevice.GetForCurrentView().MouseMoved += OnMouseMoved;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnSystemNavigationManagerBackRequested;

            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += OnDispatcherAcceleratorKeyActivated;

            Window.Current.CoreWindow.PointerPressed += OnCoreWindowPointerPressed;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsHardwareBackButtonPresent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsHardwareCameraButtonPresent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKeyboardPresent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMousePresent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTouchPresent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UseTitleBarBackButton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DeviceGestureEventArgs> CameraButtonHalfPressed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DeviceGestureEventArgs> CameraButtonPressed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DeviceGestureEventArgs> CameraButtonReleased;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DeviceGestureEventArgs> GoBackRequested;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DeviceGestureEventArgs> GoForwardRequested;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMoved;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() => throw new NotImplementedException();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnCoreWindowPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            var properties = args.CurrentPoint.Properties;

            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed || properties.IsMiddleButtonPressed)
                return;

            var backPressed = properties.IsXButton1Pressed;
            var forwardPressed = properties.IsXButton2Pressed;

            if (backPressed ^ forwardPressed)
            {
                args.Handled = true;

                if (backPressed)
                    RaiseCancelableEvent(GoBackRequested, this, new DeviceGestureEventArgs());

                if (forwardPressed)
                    RaiseCancelableEvent(GoForwardRequested, this, new DeviceGestureEventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnDispatcherAcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if ((args.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
                args.EventType == CoreAcceleratorKeyEventType.KeyDown))
            {
                var coreWindow = Window.Current.CoreWindow;
                var downState = CoreVirtualKeyStates.Down;
                var virtualKey = args.VirtualKey;
                var menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
                var winKey = ((coreWindow.GetKeyState(VirtualKey.LeftWindows) & downState) == downState || (coreWindow.GetKeyState(VirtualKey.RightWindows) & downState) == downState);
                var controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
                var shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
                var noModifiers = !menuKey && !controlKey && !shiftKey && !winKey;
                var onlyAlt = menuKey && !controlKey && !shiftKey && !winKey;

                if (((int)virtualKey == 166 && noModifiers) || (virtualKey == VirtualKey.Left && onlyAlt))
                {
                    args.Handled = true;
                    RaiseCancelableEvent(GoBackRequested, this, new DeviceGestureEventArgs());
                }
                else if (virtualKey == VirtualKey.Back && winKey)
                {
                    args.Handled = true;
                    RaiseCancelableEvent(GoBackRequested, this, new DeviceGestureEventArgs());
                }
                else if (((int)virtualKey == 167 && noModifiers) || (virtualKey == VirtualKey.Right && onlyAlt))
                {
                    args.Handled = true;
                    RaiseCancelableEvent(GoBackRequested, this, new DeviceGestureEventArgs());
                }
            }
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnHardwareButtonsBackPressed(object sender, BackPressedEventArgs e)
        {
            var args = new DeviceGestureEventArgs(false, true);

            RaiseCancelableEvent(GoBackRequested, this, args);

            e.Handled = args.Handled;
        } 
#endif

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnHardwareButtonsCameraHalfPressed(object sender, CameraEventArgs e)
            => RaiseEvent(CameraButtonHalfPressed, this, new DeviceGestureEventArgs(false, true));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnHardwareButtonsCameraPressed(object sender, CameraEventArgs e)
            => RaiseEvent(CameraButtonPressed, this, new DeviceGestureEventArgs(false, true));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnHardwareButtonsCameraReleased(object sender, CameraEventArgs e)
            => RaiseEvent(CameraButtonReleased, this, new DeviceGestureEventArgs(false, true));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected virtual void OnMouseMoved(MouseDevice sender, MouseEventArgs args)
            => RaiseEvent(MouseMoved, this, args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            var args = new DeviceGestureEventArgs();

            RaiseCancelableEvent(GoBackRequested, this, args);

            e.Handled = args.Handled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void RaiseCancelableEvent<T>(EventHandler<T> eventHandler, object sender, T args) where T : CancelEventArgs
        {
            var handler = eventHandler;

            if (handler != null)
            {
                var invocationList = handler.GetInvocationList();

                for (var i = invocationList.Length - 1; i >= 0; i--)
                {
                    var del = (EventHandler<T>)invocationList[i];

                    try
                    {
                        del(sender, args);

                        if (args.Cancel)
                            break;
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void RaiseEvent<T>(EventHandler<T> eventHandler, object sender, T args)
        {
            var handler = eventHandler;

            if (handler != null)
            {
                foreach (EventHandler<T> del in handler.GetInvocationList())
                {
                    try
                    {
                        del(sender, args);
                    }
                    catch { }
                }
            }
        }
    }
}
