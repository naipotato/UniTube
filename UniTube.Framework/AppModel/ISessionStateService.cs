using System;
using System.Collections.Generic;
using UniTube.Framework.Navigation;
using Windows.Foundation;

namespace UniTube.Framework.AppModel
{
    public interface ISessionStateService
    {
        Dictionary<string, object> SessionState { get; }

        IAsyncOperation<bool> CanRestoreSessionStateAsync();
        Dictionary<string, object> GetSessionStateForFrame(IFrameFacade frame);
        void RegisterFrame(IFrameFacade frame, string sessionStateKey);
        void RegisterKnownType(Type type);
        void RestoreFrameState();
        IAsyncAction RestoreSessionStateAsync();
        IAsyncAction SaveAsync();
        void UnregisterFrame(IFrameFacade frame);
    }
}
