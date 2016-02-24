using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Calls;
using Windows.ApplicationModel.Chat;
using Windows.ApplicationModel.Email;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.IncrementalLoading;
using Findier.Windows.Services;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class PostViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private readonly IInsightsService _insightsService;
        private CommentCollection _commentCollection;
        private Post _post;

        public PostViewModel(IFindierService findierService, IInsightsService insightsService)
        {
            _findierService = findierService;
            _insightsService = insightsService;

            CallCommand = new DelegateCommand(CallExecute);
            TextCommand = new DelegateCommand(TextExecute);
            EmailCommand = new DelegateCommand(EmailExecute);
            NewCommentCommand = new DelegateCommand(NewCommentExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        public DelegateCommand CallCommand { get; }

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

        public DelegateCommand EmailCommand { get; }

        public DelegateCommand NewCommentCommand { get; }

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

        public DelegateCommand TextCommand { get; }

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

        private void CallExecute()
        {
            PhoneCallManager.ShowPhoneCallUI(Post.PhoneNumber, "@" + Post.User);

            _insightsService.TrackEvent("ContactCall",
                new Dictionary<string, string>
                {
                    { "PostId", Post.Id }
                });
        }

        private async void EmailExecute()
        {
            var mail = new EmailMessage
            {
                Subject = "Findier post",
                Body = $"Contacting you in regards to your post on Findier: \"{Post.Title}\"\n\n"
            };
            mail.To.Add(new EmailRecipient(Post.Email, "@" + Post.User));
            await EmailManager.ShowComposeNewEmailAsync(mail);

            _insightsService.TrackEvent("ContactEmail",
                new Dictionary<string, string>
                {
                    { "PostId", Post.Id }
                });
        }

        private void NewCommentExecute()
        {
            NavigationService.Navigate(typeof (NewCommentPage), Post.Id);
        }

        private async void TextExecute()
        {
            var msg = new ChatMessage
            {
                Body = $"Contacting you in regards to your post on Findier: \"{Post.Title}\""
            };
            msg.Recipients.Add(Post.PhoneNumber);
            await ChatMessageManager.ShowComposeSmsMessageAsync(msg);

            _insightsService.TrackEvent("ContactSms",
                new Dictionary<string, string>
                {
                    { "PostId", Post.Id }
                });
        }
    }
}