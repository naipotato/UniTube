using System.ComponentModel;

namespace UniTube.Framework.AppModel
{
    public class DeviceGestureEventArgs : CancelEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public DeviceGestureEventArgs() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handled"></param>
        public DeviceGestureEventArgs(bool handled)
        {
            Handled = handled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handled"></param>
        /// <param name="isHardwareButton"></param>
        public DeviceGestureEventArgs(bool handled, bool isHardwareButton)
        {
            Handled = handled;
            IsHardwareButton = isHardwareButton;
        }

        /// <summary>
        /// Gets
        /// </summary>
        public bool IsHardwareButton { get; }

        /// <summary>
        /// Gets or sets
        /// </summary>
        public bool Handled { get; set; }
    }
}
