using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Enums;
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
        private PostsCollection _hotPostsCollection;
        private PostsCollection _newPostsCollection;
        private PostsCollection _topPostsCollection;

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

        public PostsCollection HotPostsCollection
        {
            get
            {
                return _hotPostsCollection;
            }
            set
            {
                Set(ref _hotPostsCollection, value);
            }
        }

        public DelegateCommand NewPostCommand { get; }

        public PostsCollection NewPostsCollection
        {
            get
            {
                return _newPostsCollection;
            }
            set
            {
                Set(ref _newPostsCollection, value);
            }
        }

        public DelegateCommand<ItemClickEventArgs> PostClickCommand { get; }

        public PostsCollection TopPostsCollection
        {
            get
            {
                return _topPostsCollection;
            }
            set
            {
                Set(ref _topPostsCollection, value);
            }
        }

        public override sealed void OnNavigatedTo(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> state)
        {
            Category = (Category)parameter;

            HotPostsCollection = new PostsCollection(new GetPostsRequest(Category.Id).Sort(PostSort.Hot),
                _findierService);
            NewPostsCollection = new PostsCollection(new GetPostsRequest(Category.Id).Sort(PostSort.New),
                _findierService);
            TopPostsCollection = new PostsCollection(new GetPostsRequest(Category.Id).Sort(PostSort.Top),
                _findierService);
        }

        private void NewPostExecute()
        {
            NavigationService.Navigate(typeof (NewPostPage), Category);
        }

        private void PostClickExecute(ItemClickEventArgs args)
        {
            var post = (Post)args.ClickedItem;
            NavigationService.Navigate(typeof (PostPage), post);
        }
    }
}