using System.Collections.Generic;
using System.Globalization;
using Windows.UI.Xaml.Navigation;
using Findier.Core.Extensions;
using Findier.Web.Enums;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.Services;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class EditPostViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private readonly IInsightsService _insightsService;

        private bool _canEditNsfw;

        private string _email;
        private bool _isFixed;
        private bool _isFreebie;
        private bool _isLoading;
        private bool _isMoney = true;
        private bool _isNsfw;
        private string _phoneNumber;

        private Post _post;
        private string _price;
        private string _text;
        private string _title;

        public EditPostViewModel(IFindierService findierService, IInsightsService insightsService)
        {
            _findierService = findierService;
            _insightsService = insightsService;

            EditCommand = new DelegateCommand(EditExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(new Post
                {
                    Title = "5 dollar app icons",
                    Text =
                        "Studying gfx design and trying to do some freelance work on the side.\n\nCan do flat and minimilistic look. Text or icon based.",
                    Email = "doe@test.com",
                    IsNsfw = true
                },
                    NavigationMode.New,
                    new Dictionary<string, object>());
            }
        }

        public bool CanEditNsfw
        {
            get
            {
                return _canEditNsfw;
            }
            set
            {
                Set(ref _canEditNsfw, value);
            }
        }

        public DelegateCommand EditCommand { get; }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Set(ref _email, value);
            }
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

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                Set(ref _phoneNumber, value);
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

        public override sealed void OnNavigatedTo(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> state)
        {
            _post = (Post)parameter;
            CanEditNsfw = !_post.IsNsfw;
            Title = _post.Title;
            Text = _post.Text;
            IsNsfw = _post.IsNsfw;
            Price = _post.Price.ToString(CultureInfo.InvariantCulture);
            Email = _post.Email;
            PhoneNumber = _post.PhoneNumber;

            IsMoney = _post.Type == PostType.Money;
            IsFixed = _post.Type == PostType.Fixed;
            IsFreebie = _post.Type == PostType.Freebie;
        }

        private async void EditExecute()
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
                if (!decimal.TryParse(Price, out price) || price < 1)
                {
                    CurtainPrompt.ShowError("Please enter a valid price.");
                    return;
                }
            }
            else if (IsMoney)
            {
                type = PostType.Money;
            }

            if (!string.IsNullOrWhiteSpace(Email) && !Email.IsEmail())
            {
                CurtainPrompt.ShowError("Please enter a valid email.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(PhoneNumber) && !PhoneNumber.IsPhoneNumber())
            {
                CurtainPrompt.ShowError("Please enter a valid phone number.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email)
                && string.IsNullOrWhiteSpace(PhoneNumber))
            {
                CurtainPrompt.ShowError("Please enter at least one contact method.");
                return;
            }

            var editPostRequest = new EditPostRequest(_post.Id,
                _post.Text == Text ? null : Text,
                _post.Type == type ? null : (PostType?)type,
                _post.Price == price ? null : (decimal?)price,
                _post.IsNsfw == IsNsfw || !CanEditNsfw ? null : (bool?)IsNsfw,
                _post.Email == Email ? null : Email,
                _post.PhoneNumber == PhoneNumber ? null : PhoneNumber);

            IsLoading = true;
            var restResponse = await _findierService.SendAsync(editPostRequest);
            IsLoading = false;

            if (restResponse.IsSuccessStatusCode)
            {
                CurtainPrompt.Show("Edit was saved.");
                NavigationService.GoBack();
            }
            else
            {
                CurtainPrompt.ShowError(restResponse.DeserializedResponse?.Error
                    ?? "Problem editing post. Try again later.");
            }

            var props = new Dictionary<string, string>();

            if (restResponse.IsSuccessStatusCode)
            {
                props.Add("Error", restResponse.DeserializedResponse?.Error ?? "Unknown");
                props.Add("StatusCode", restResponse.StatusCode.ToString());
            }
            _insightsService.TrackEvent("EditPost", props);
        }
    }
}