namespace UniTube.ViewModels
{
    public class VMLocator
    {
        private HomeViewModel _homeViewModel;
        public HomeViewModel HomeViewModel =>
            _homeViewModel ?? (_homeViewModel = new HomeViewModel());

        private MasterViewModel _masterViewModel;
        public MasterViewModel MasterViewModel =>
            _masterViewModel ?? (_masterViewModel = new MasterViewModel());

        private SearchViewModel _searchViewModel;
        public SearchViewModel SearchViewModel =>
            _searchViewModel ?? (_searchViewModel = new SearchViewModel());

        private SettingsViewModel _settingsViewModel;
        public SettingsViewModel SettingsViewModel =>
            _settingsViewModel ?? (_settingsViewModel = new SettingsViewModel());
    }
}
