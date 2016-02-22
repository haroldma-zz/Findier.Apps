using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Findier.Client.Windows.Common;
using Findier.Client.Windows.Engine.Mvvm;
using Findier.Client.Windows.IncrementalLoading;
using Findier.Client.Windows.Views;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;

namespace Findier.Client.Windows.ViewModels
{
    public class PostViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private CommentCollection _commentCollection;
        private Post _post;

        public PostViewModel(IFindierService findierService)
        {
            _findierService = findierService;

            MessageCommand = new DelegateCommand(MessageExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        public CommentCollection CommentCollection
        {
            get
            {
                return _commentCollection;
            }
            set
            {
                Set(ref _commentCollection, value);
            }
        }

        public DelegateCommand MessageCommand { get; }

        public Post Post
        {
            get
            {
                return _post;
            }
            set
            {
                Set(ref _post, value);
            }
        }

        public override sealed async void OnNavigatedTo(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> state)
        {
            var postResponse =
                await _findierService.SendAsync<GetPostRequest, Post>(new GetPostRequest(parameter as string));
            if (!postResponse.IsSuccessStatusCode)
            {
                CurtainPrompt.ShowError(postResponse.DeserializedResponse?.Error ?? "Problem loading post.");
                NavigationService.GoBack();
                return;
            }

            Post = postResponse.DeserializedResponse.Data;
            var commentsRequest = new GetPostCommentsRequest(parameter as string).Limit(20);
            CommentCollection = new CommentCollection(commentsRequest, _findierService);
        }

        private void MessageExecute()
        {
            if (_findierService.IsAuthenticated)
            {
                // go to message page
            }
            else
            {
                NavigationService.Navigate(typeof (AuthenticationPage), true);
            }
        }
    }
}