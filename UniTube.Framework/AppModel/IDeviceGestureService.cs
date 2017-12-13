using System;
using Windows.Devices.Input;

namespace UniTube.Framework.AppModel
{
    public interface IDeviceGestureService
    {
        bool IsHardwareBackButtonPresent { get; }
        bool IsHardwareCameraButtonPresent { get; }
        bool IsKeyboardPresent { get; }
        bool IsMousePresent { get; }
        bool IsTouchPresent { get; }
        bool UseTitleBarBackButton { get; set; }

        event EventHandler<DeviceGestureEventArgs> CameraButtonHalfPressed;
        event EventHandler<DeviceGestureEventArgs> CameraButtonPressed;
        event EventHandler<DeviceGestureEventArgs> CameraButtonReleased;
        event EventHandler<DeviceGestureEventArgs> GoBackRequested;
        event EventHandler<DeviceGestureEventArgs> GoForwardRequested;
        event EventHandler<MouseEventArgs> MouseMoved;
    }
}
