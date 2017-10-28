using System.Collections.Generic;
using System.Threading.Tasks;

using Template10.Mvvm;

using UniTube.Collections;
using UniTube.Controls;
using UniTube.Core.Resources;
using UniTube.Sources;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace UniTube.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private SearchSource _searchSource;
        private string _query = string.Empty;

        public IncrementalLoadingCollection<SearchSource, SearchResult> SearchList { get; private set; }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if ((parameter as string) != _query)
            {
                _query = parameter as string;
                var loading = Window.Current.Content as LoadingView;

                _searchSource = new SearchSource(_query, () =>
                {
                    loading.IsLoading = true;
                },
                () =>
                {
                    loading.IsLoading = false;
                });
                SearchList = new IncrementalLoadingCollection<SearchSource, SearchResult>(_searchSource,
                    onError: (ex) =>
                    {
                        loading.IsLoading = false;
                    });
            }
            return Task.CompletedTask;
        }
    }
}