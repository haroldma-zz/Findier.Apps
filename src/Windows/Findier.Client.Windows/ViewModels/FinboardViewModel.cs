using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Findier.Client.Windows.Engine.Mvvm;
using Findier.Client.Windows.IncrementalLoading;
using Findier.Client.Windows.Views;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;

namespace Findier.Client.Windows.ViewModels
{
    public class FinboardViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private PlainPostCollection _postCollection;

        public FinboardViewModel(IFindierService findierService)
        {
            _findierService = findierService;
            PostClickCommand = new DelegateCommand<ItemClickEventArgs>(PostClickExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        public DelegateCommand<ItemClickEventArgs> PostClickCommand { get; }

        public PlainPostCollection PostCollection
        {
            get
            {
                return _postCollection;
            }
            set
            {
                Set(ref _postCollection, value);
            }
        }

        public override sealed void OnNavigatedTo(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> state)
        {
            var postsRequest = new GetFinboardFeedRequest(parameter as string).Limit(20);
            PostCollection = new PlainPostCollection(postsRequest, _findierService);
        }

        private void PostClickExecute(ItemClickEventArgs args)
        {
            var post = (PlainPost)args.ClickedItem;
            NavigationService.Navigate(typeof (PostPage), post.Id);
        }
    }
}