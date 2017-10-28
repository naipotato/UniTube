using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Template10.Mvvm;
using Template10.Services.NavigationService;

using UniTube.Controls;
using UniTube.Helpers;
using UniTube.Views;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using UniTube.Collections;
using UniTube.Core.Requests;
using Windows.System.UserProfile;

namespace UniTube.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        #region Private Variables
        private HamburgerMenuDisplayMode _displayMode;
        private bool _isLoggedIn;
        private bool _isPaneOpen;
        private bool _isSearchModeEnabled;
        private DelegateCommand<string> _menuClickCommand;
        private string _pageTitle;
        private string _selectedPage;
        private ObservableCollection<string> _suggestionsList;
        #endregion

        #region Properties
        public Type DefaultPageType => IsLoggedIn ? typeof(HomePage) : typeof(TrendingPage);

        public HamburgerMenuDisplayMode DisplayMode
        {
            get => _displayMode;
            set => Set(ref _displayMode, value);
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => Set(ref _isLoggedIn, value);
        }

        public bool IsPaneOpen
        {
            get => _isPaneOpen;
            set => Set(ref _isPaneOpen, value);
        }

        public bool IsSearchModeEnabled
        {
            get => _isSearchModeEnabled;
            set => Set(ref _isSearchModeEnabled, value);
        }

        public INavigationService MasterNavigationService { get; set; }

        public string PageTitle
        {
            get => _pageTitle;
            set => Set(ref _pageTitle, value);
        }

        public string SelectedPage
        {
            get => _selectedPage;
            set => Set(ref _selectedPage, value);
        }

        public ObservableCollection<string> SuggestionsList => _suggestionsList;

        public DelegateCommand<string> MenuClickCommand
            => _menuClickCommand ?? (_menuClickCommand = new DelegateCommand<string>(MenuClick, MenuClickCommandCanExecute));
        #endregion

        public MasterViewModel()
        {
            IsLoggedIn = false;
            _suggestionsList = new ObservableCollection<string>();
        }

        #region Methods
        public void GoBack() => MasterNavigationService.GoBack();

        public void LogIn()
        {
            
        }

        private bool MenuClickCommandCanExecute(string arg) => !arg.IsNullOrEmpty();

        private void MenuClick(string arg)
        {
            Enum.TryParse(arg, out Pages page);
            IsPaneOpen = DisplayMode == HamburgerMenuDisplayMode.Expanded;
            MasterNavigationService.Navigate(page, infoOverride: new DrillInNavigationTransitionInfo());
        }

        public void OpenClosePane() => IsPaneOpen = !IsPaneOpen;

        public void Search(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
        }

        public async void UpdateSuggestions(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                SuggestionsList.Clear();
                if (!sender.Text.IsNullOrEmpty())
                {
                    sender.IsSuggestionListOpen = true;

                    var list = await new SuggestionListRequest(sender.Text, SuggestionListRequest.ClientTypeEnum.Firefox)
                    {
                        Hl = GlobalizationPreferences.Languages[0],
                        Ds = "yt"
                    }.ExecuteAsync();

                    foreach (var item in list)
                    {
                        SuggestionsList.Add(item);
                    }
                }
                else
                {
                    sender.IsSuggestionListOpen = false;
                }
            }
        }

        public void UpdatePageSelected(object sender, NavigatedEventArgs args)
        {
            UpdatePageTitle(args.PageType.Name);
            SelectedPage = args.PageType.Name;
            IsSearchModeEnabled = (args.PageType.Name == "SearchPage");
        }

        private void UpdatePageTitle(string obj)
        {
            switch (obj)
            {
                case "HistoryPage":
                    PageTitle = "History";
                    break;
                case "HomePage":
                    PageTitle = "Home";
                    break;
                case "SavedPage":
                    PageTitle = "Saved";
                    break;
                case "SettingsPage":
                    PageTitle = "Settings";
                    break;
            }
        }
        #endregion
    }
}
