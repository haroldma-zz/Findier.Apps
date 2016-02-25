﻿using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Enums;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.IncrementalLoading;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private CategoriesCollection _categoriesCollection;
        private PlainPostsCollection _newPostsCollection;
        private PlainPostsCollection _topPostsCollection;

        public MainViewModel(IFindierService findierService)
        {
            FindierService = findierService;
            CategoryClickCommand = new DelegateCommand<ItemClickEventArgs>(CategoryClickExecute);
            PostClickCommand = new DelegateCommand<ItemClickEventArgs>(PostClickExecute);
            LoginCommand = new DelegateCommand(LoginExecute);
            LogoutCommand = new DelegateCommand(LogoutExecute);
            ContactCommand = new DelegateCommand(ContactExecute);
            ReviewCommand = new DelegateCommand(ReviewExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        private void PostClickExecute(ItemClickEventArgs args)
        {
            var post = (PlainPost)args.ClickedItem;
            NavigationService.Navigate(typeof(PostPage), post.Id);
        }

        public DelegateCommand<ItemClickEventArgs> PostClickCommand { get; }

        public CategoriesCollection CategoriesCollection
        {
            get
            {
                return _categoriesCollection;
            }
            set
            {
                Set(ref _categoriesCollection, value);
            }
        }

        public DelegateCommand<ItemClickEventArgs> CategoryClickCommand { get; }

        public DelegateCommand ContactCommand { get; }

        public IFindierService FindierService { get; }

        public DelegateCommand LoginCommand { get; }

        public DelegateCommand LogoutCommand { get; }

        public PlainPostsCollection NewPostsCollection
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

        public DelegateCommand ReviewCommand { get; }

        public PlainPostsCollection TopPostsCollection
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
            CategoriesCollection = new CategoriesCollection(new GetCategoriesRequest(Country.PR).Limit(20),
                FindierService);
            NewPostsCollection = new PlainPostsCollection(new GetPostsRequest(PostSort.New).Limit(20), FindierService);
            TopPostsCollection = new PlainPostsCollection(new GetPostsRequest(PostSort.Top).Limit(20), FindierService);
        }

        private void CategoryClickExecute(ItemClickEventArgs args)
        {
            var finboard = (Category)args.ClickedItem;
            NavigationService.Navigate(typeof (CategoryPage), finboard);
        }

        private async void ContactExecute()
        {
            var mail = new EmailMessage
            {
                Subject = "Findier for Windows"
            };
            mail.To.Add(new EmailRecipient("help@zumicts.com", "Zumicts Support"));
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private void LoginExecute()
        {
            NavigationService.Navigate(typeof (AuthenticationPage), clearBackStack: true);
        }

        private void LogoutExecute()
        {
            FindierService.Logout();
            NavigationService.Navigate(typeof (AuthenticationPage), clearBackStack: true);
            CurtainPrompt.Show("Goodbye!");
        }

        private async void ReviewExecute()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }
    }
}