using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.Services;

namespace Findier.Windows.ViewModels
{
    public class NewCommentViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private readonly IInsightsService _insightsService;
        private bool _isLoading;
        private string _postId;
        private string _text;

        public NewCommentViewModel(IFindierService findierService, IInsightsService insightsService)
        {
            _findierService = findierService;
            _insightsService = insightsService;

            PublishCommand = new DelegateCommand(PublishExecute);
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(ref _isLoading, value);
            }
        }

        public DelegateCommand PublishCommand { get; }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                Set(ref _text, value);
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _postId = parameter as string;
        }

        private async void PublishExecute()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                CurtainPrompt.ShowError("Looks like you forgot to include some text.");
                return;
            }

            var newCommentRequest = new CreateCommentRequest(_postId, Text);

            IsLoading = true;
            var restResponse = await _findierService.SendAsync<CreateCommentRequest, string>(newCommentRequest);
            IsLoading = false;

            if (restResponse.IsSuccessStatusCode)
            {
                CurtainPrompt.Show("Comment published!");
                NavigationService.GoBack();
            }
            else
            {
                CurtainPrompt.ShowError(restResponse.DeserializedResponse?.Error ?? "Problem publishing comment.");
            }
        }
    }
}