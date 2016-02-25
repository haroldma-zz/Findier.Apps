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
    public class CategoryViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private Category _category;
        private PlainPostsCollection _postsCollection;

        public CategoryViewModel(IFindierService findierService)
        {
            _findierService = findierService;
            PostClickCommand = new DelegateCommand<ItemClickEventArgs>(PostClickExecute);
            NewPostCommand = new DelegateCommand(NewPostExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        public Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                Set(ref _category, value);
            }
        }

        public DelegateCommand NewPostCommand { get; }

        public DelegateCommand<ItemClickEventArgs> PostClickCommand { get; }

        public PlainPostsCollection PostsCollection
        {
            get
            {
                return _postsCollection;
            }
            set
            {
                Set(ref _postsCollection, value);
            }
        }

        public override sealed void OnNavigatedTo(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> state)
        {
            Category = (Category)parameter;
            var postsRequest = new GetPostsRequest(Category.Id).Limit(20);
            PostsCollection = new PlainPostsCollection(postsRequest, _findierService);
        }

        private void NewPostExecute()
        {
            NavigationService.Navigate(typeof (NewPostPage), Category);
        }

        private void PostClickExecute(ItemClickEventArgs args)
        {
            var post = (PlainPost)args.ClickedItem;
            NavigationService.Navigate(typeof (PostPage), post.Id);
        }
    }
}