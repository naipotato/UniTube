using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace UniTube.Collections
{
    public class IncrementalLoadingCollection<TSource, IType> : ObservableCollection<IType>,
        ISupportIncrementalLoading
        where TSource : IIncrementalSource<IType>
    {
        public Action OnStartLoading { get; set; }

        public Action OnEndLoading { get; set; }

        public Action<Exception> OnError { get; set; }

        protected TSource Source { get; }

        protected int ItemsPerPage { get; }

        protected int CurrentPageIndex { get; set; }

        private bool _isLoading;
        private bool _hasMoreItems;
        private CancellationToken _cancellationToken;
        private bool _refreshOnLoad;

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (value != _isLoading)
                {
                    _isLoading = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsLoading)));

                    if (_isLoading)
                    {
                        OnStartLoading?.Invoke();
                    }
                    else
                    {
                        OnEndLoading?.Invoke();
                    }
                }
            }
        }

        public bool HasMoreItems
        {
            get
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                return _hasMoreItems;
            }
            private set
            {
                if (value != _hasMoreItems)
                {
                    _hasMoreItems = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(HasMoreItems)));
                }
            }
        }

        public IncrementalLoadingCollection(int itemsPerPage = 20, Action onStartLoading = null, Action onEndLoading = null, Action<Exception> onError = null)
            : this(Activator.CreateInstance<TSource>(), itemsPerPage, onStartLoading, onStartLoading, onError)
        {
        }

        public IncrementalLoadingCollection(TSource source, int itemsPerPage = 20, Action onStartLoading = null, Action onEndLoading = null, Action<Exception> onError = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Source = source;

            OnStartLoading = onStartLoading;
            OnEndLoading = onEndLoading;
            OnError = onError;

            ItemsPerPage = itemsPerPage;
            _hasMoreItems = true;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            => LoadMoreItemsAsync(count, new CancellationToken(false)).AsAsyncOperation();

        public async Task RefreshAsync()
        {
            if (IsLoading)
            {
                _refreshOnLoad = true;
            }
            else
            {
                Clear();
                CurrentPageIndex = 0;
                HasMoreItems = true;
                await LoadMoreItemsAsync(1);
            }
        }

        protected virtual async Task<IEnumerable<IType>> LoadDataAsync(CancellationToken cancellationToken)
        {
            var result = await Source.GetPagedItemsAsync(CurrentPageIndex++, ItemsPerPage, cancellationToken);
            return result;
        }

        private async Task<LoadMoreItemsResult> LoadMoreItemsAsync(uint count, CancellationToken cancellationToken)
        {
            uint resultCount = 0;
            _cancellationToken = cancellationToken;

            try
            {
                if (!_cancellationToken.IsCancellationRequested)
                {
                    IEnumerable<IType> data = null;
                    try
                    {
                        IsLoading = true;
                        data = await LoadDataAsync(_cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {

                    }
                    catch (Exception ex) when (OnError != null)
                    {
                        OnError.Invoke(ex);
                    }

                    if (data != null && data.Any() && !_cancellationToken.IsCancellationRequested)
                    {
                        resultCount = (uint)data.Count();

                        foreach (var item in data)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        HasMoreItems = false;
                    }
                }
            }
            finally
            {
                IsLoading = false;

                if (_refreshOnLoad)
                {
                    _refreshOnLoad = false;
                    await RefreshAsync();
                }
            }

            return new LoadMoreItemsResult { Count = resultCount };
        }
    }
}
