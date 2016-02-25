using System.Collections.ObjectModel;
using Findier.Web.Enums;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Responses;
using Findier.Web.Services;
using MvvmCross.Core.ViewModels;

namespace Findier.Android.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IFindierService _findierService;
        private ObservableCollection<Category> _categories;

        public MainViewModel(IFindierService findierService)
        {
            _findierService = findierService;
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                SetProperty(ref _categories, value);
            }
        }

        public override async void Start()
        {
            base.Start();
            var categoriesRequest = new GetCategoriesRequest(Country.PR);
            var restResponse =
                await _findierService.SendAsync<GetCategoriesRequest, FindierPageData<Category>>(categoriesRequest);
            if (restResponse.IsSuccessStatusCode)
            {
                Categories = new ObservableCollection<Category>(restResponse.DeserializedResponse.Data.Results);
            }
        }
    }
}