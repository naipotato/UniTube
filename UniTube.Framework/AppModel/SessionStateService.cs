using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UniTube.Framework.Navigation;
using Windows.Foundation;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.UI.Xaml;

namespace UniTube.Framework.AppModel
{
    public class SessionStateService : ISessionStateService
    {
        private Dictionary<string, object> _sessionState = new Dictionary<string, object>();
        private readonly List<Type> _knownTypes = new List<Type>();

        private static readonly List<WeakReference<IFrameFacade>> RegisteredFrames = new List<WeakReference<IFrameFacade>>();

        private static readonly DependencyProperty FrameSessionStateKeyProperty = DependencyProperty.RegisterAttached(
            "_FrameSessionStateKey", typeof(string), typeof(SessionStateService), null);
        private static readonly DependencyProperty FrameSessionStateProperty = DependencyProperty.RegisterAttached(
            "_FrameSessionState", typeof(Dictionary<string, object>), typeof(SessionStateService), null);

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> SessionState => _sessionState;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAsyncOperation<bool> CanRestoreSessionStateAsync() => Task.Run(async () =>
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("_sessionState.xml");
                return true;
            }
            catch
            {
                return false;
            }
        }).AsAsyncOperation();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetSessionStateForFrame(IFrameFacade frame)
        {
            if (frame == null) throw new ArgumentNullException(nameof(frame));

            var frameState = (Dictionary<string, object>)frame.GetValue(FrameSessionStateProperty);

            if (frameState == null)
            {
                var frameSessionKey = (string)frame.GetValue(FrameSessionStateKeyProperty);
                if (frameSessionKey == null)
                {
                    if (!_sessionState.ContainsKey(frameSessionKey))
                    {
                        _sessionState[frameSessionKey] = new Dictionary<string, object>();
                    }
                    frameState = (Dictionary<string, object>)_sessionState[frameSessionKey];
                }
                else
                {
                    frameState = new Dictionary<string, object>();
                }
                frame.SetValue(FrameSessionStateProperty, frameState);
            }
            return frameState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="sessionStateKey"></param>
        public void RegisterFrame(IFrameFacade frame, string sessionStateKey)
        {
            if (frame == null)
                throw new ArgumentNullException(nameof(frame));

            if (frame.GetValue(FrameSessionStateKeyProperty) != null)
            {
                throw new InvalidOperationException("Frames can only be registered to one session state key");
            }

            if (frame.GetValue(FrameSessionStateProperty) != null)
            {
                throw new InvalidOperationException("Frames must be either be registered before accessing frame session state, or not registered at all");
            }

            frame.SetValue(FrameSessionStateKeyProperty, sessionStateKey);
            RegisteredFrames.Add(new WeakReference<IFrameFacade>(frame));

            RestoreFrameNavigationState(frame);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void RegisterKnownType(Type type) => _knownTypes.Add(type);

        private void RestoreFrameNavigationState(IFrameFacade frame)
        {
            var frameState = GetSessionStateForFrame(frame);
            if (frameState.ContainsKey("Navigation"))
            {
                frame.SetNavigationState((string)frameState["Navigation"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RestoreFrameState()
        {
            foreach (var weakFrameReference in RegisteredFrames)
            {
                if (weakFrameReference.TryGetTarget(out var frame))
                {
                    frame.ClearValue(FrameSessionStateProperty);
                    RestoreFrameNavigationState(frame);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAsyncAction RestoreSessionStateAsync() => Task.Run(async () =>
        {
            _sessionState = new Dictionary<string, object>();

            var file = await ApplicationData.Current.LocalFolder.GetFileAsync("_sessionState.xml");
            using (var inStream = await file.OpenSequentialReadAsync())
            {
                var memoryStream = new MemoryStream();
                var provider = new DataProtectionProvider("LOCAL=user");

                await provider.UnprotectStreamAsync(inStream, memoryStream.AsOutputStream());
                memoryStream.Seek(0, SeekOrigin.Begin);
                var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), _knownTypes);
                _sessionState = (Dictionary<string, object>)serializer.ReadObject(memoryStream);
            }
        }).AsAsyncAction();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAsyncAction SaveAsync() => Task.Run(async () =>
        {
            foreach (var weakFrameReference in RegisteredFrames)
            {
                if (weakFrameReference.TryGetTarget(out var frame))
                {
                    SaveFrameNavigationState(frame);
                }
            }

            var sessionData = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(Dictionary<string, object>), _knownTypes);
            serializer.WriteObject(sessionData, _sessionState);

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("_sessionState.xml", CreationCollisionOption.ReplaceExisting);
            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                sessionData.Seek(0, SeekOrigin.Begin);
                var provider = new DataProtectionProvider("LOCAL=user");

                await provider.ProtectStreamAsync(sessionData.AsInputStream(), fileStream);
                await fileStream.FlushAsync();
            }
        }).AsAsyncAction();

        private void SaveFrameNavigationState(IFrameFacade frame)
        {
            var frameState = GetSessionStateForFrame(frame);
            frameState["Navigation"] = frame.GetNavigationState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        public void UnregisterFrame(IFrameFacade frame)
        {
            SessionState.Remove((string)frame.GetValue(FrameSessionStateKeyProperty));
            RegisteredFrames.RemoveAll((weakFrameReference)
                => !weakFrameReference.TryGetTarget(out var testFrame) || testFrame == frame);
        }
    }
}
