using Template10.Common;
using Template10.Mvvm;

using UniTube.Collections;
using UniTube.Controls;
using UniTube.Core.Resources;
using UniTube.Sources;

using Windows.UI.Xaml;

namespace UniTube.ViewModels
{
    public class TrendingViewModel : ViewModelBase
    {
        private TrendingSource trendingSource;

        public IncrementalLoadingCollection<TrendingSource, Video> TrendingList { get; }
        public VMLocator Locator => (BootStrapper.Current.Resources["Locator"] as VMLocator);

        public TrendingViewModel()
        {
            var loading = Window.Current.Content as LoadingView;

            trendingSource = new TrendingSource(() =>
            {
                loading.IsLoading = true;
            },
            () =>
            {
                loading.IsLoading = false;
            });
            TrendingList = new IncrementalLoadingCollection<TrendingSource, Video>(trendingSource,
                onError: (ex) =>
                {
                    loading.IsLoading = false;
                });
        }
    }
}
