using System.Collections.Generic;

namespace UniTube.Framework.Navigation
{
    public interface INavigationAware
    {
        void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState);
        void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending);
    }
}
