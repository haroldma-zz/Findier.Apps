using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.IncrementalLoading;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class FinboardViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private Finboard _finboard;
        private PlainPostCollection _postCollection;

        public FinboardViewModel(IFindierService findierService)
        {
            _findierService = findierService;
            PostClickCommand = new DelegateCommand<ItemClickEventArgs>(PostClickExecute);
            NewPostCommand = new DelegateCommand(NewPostExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        public DelegateCommand NewPostCommand { get; }

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
            _finboard = (Finboard)parameter;
            var postsRequest = new GetFinboardFeedRequest(_finboard.Id).Limit(20);
            PostCollection = new PlainPostCollection(postsRequest, _findierService);
        }

        private void NewPostExecute()
        {
            NavigationService.Navigate(typeof (NewPostPage), _finboard);
        }

        private void PostClickExecute(ItemClickEventArgs args)
        {
            var post = (PlainPost)args.ClickedItem;
            NavigationService.Navigate(typeof (PostPage), post.Id);
        }
    }
}