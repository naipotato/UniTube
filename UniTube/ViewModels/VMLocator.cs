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

        private TrendingViewModel _trendingViewModel;
        public TrendingViewModel TrendingViewModel =>
            _trendingViewModel ?? (_trendingViewModel = new TrendingViewModel());

        private VideoViewModel _videoViewModel;
        public VideoViewModel VideoViewModel =>
            _videoViewModel ?? (_videoViewModel = new VideoViewModel());
    }
}
