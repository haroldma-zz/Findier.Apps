using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Findier.Windows.Common
{
    public abstract class IncrementalLoadingBase<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private bool _isLoading;

        public bool HasMoreItems => HasMoreItemsOverride();

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            private set
            {
                _isLoading = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsLoading"));
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (IsLoading)
            {
                throw new InvalidOperationException("Only one operation in flight at a time");
            }

            IsLoading = true;

            return AsyncInfo.Run(c => LoadMoreItemsAsync(c, count));
        }

        protected abstract bool HasMoreItemsOverride();

        protected abstract Task<IList<T>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count);

        private async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken c, uint count)
        {
            try
            {
                var items = await LoadMoreItemsOverrideAsync(c, count);

                if (items == null)
                {
                    return new LoadMoreItemsResult();
                }

                foreach (var item in items)
                {
                    Add(item);
                }

                return new LoadMoreItemsResult { Count = (uint)items.Count };
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}