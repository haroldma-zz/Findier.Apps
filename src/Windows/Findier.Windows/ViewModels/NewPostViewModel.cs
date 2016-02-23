using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Enums;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class NewPostViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private PlainFinboard _finboard;
        private bool _isFixed;
        private bool _isFreebie;
        private bool _isLoading;
        private bool _isMoney = true;
        private bool _isNsfw;
        private string _price;
        private string _text;
        private string _title;

        public NewPostViewModel(IFindierService findierService)
        {
            _findierService = findierService;

            PublishCommand = new DelegateCommand(PublishExecute);
        }

        public bool IsFixed
        {
            get
            {
                return _isFixed;
            }
            set
            {
                Set(ref _isFixed, value);
            }
        }

        public bool IsFreebie
        {
            get
            {
                return _isFreebie;
            }
            set
            {
                Set(ref _isFreebie, value);
            }
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

        public bool IsMoney
        {
            get
            {
                return _isMoney;
            }
            set
            {
                Set(ref _isMoney, value);
            }
        }

        public bool IsNsfw
        {
            get
            {
                return _isNsfw;
            }
            set
            {
                Set(ref _isNsfw, value);
            }
        }

        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                Set(ref _price, value);
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

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                Set(ref _title, value);
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _finboard = (PlainFinboard)parameter;
        }

        private async void PublishExecute()
        {
            if (!_findierService.IsAuthenticated)
            {
                NavigationService.Navigate(typeof (AuthenticationPage), true);
                return;
            }

            var type = PostType.Freebie;
            decimal price = 0;
            if (IsFixed)
            {
                type = PostType.Fixed;
                // convert the price into a decimal
                if (!decimal.TryParse(Price, out price) || price <= 0)
                {
                    CurtainPrompt.ShowError("Please enter a valid price.");
                    return;
                }
            }
            else if (IsMoney)
            {
                type = PostType.Money;
            }

            if (string.IsNullOrWhiteSpace(Title))
            {
                CurtainPrompt.ShowError("Don't forget to set a title!");
                return;
            }
            if (Title.Length < 5)
            {
                CurtainPrompt.ShowError("The title is a bit too short.");
                return;
            }

            var createPostRequest = new CreatePostRequest(_finboard.Id, Title, Text, type, price, IsNsfw);

            IsLoading = true;
            var restResponse = await _findierService.SendAsync<CreatePostRequest, string>(createPostRequest);
            IsLoading = false;

            if (restResponse.IsSuccessStatusCode)
            {
                CurtainPrompt.Show("You post is live!");
                NavigationService.GoBack();
            }
            else
            {
                CurtainPrompt.ShowError(restResponse.DeserializedResponse?.Error
                    ?? "Problem publishing post. Try again later.");
            }
        }
    }
}