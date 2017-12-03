namespace UniTube.ViewModels
{
    public class VMLocator
    {
        private HistoryViewModel _historyViewModel;
        public HistoryViewModel HistoryViewModel =>
            _historyViewModel ?? (_historyViewModel = new HistoryViewModel());

        private HomeViewModel _homeViewModel;
        public HomeViewModel HomeViewModel =>
            _homeViewModel ?? (_homeViewModel = new HomeViewModel());

        private MasterViewModel _masterViewModel;
        public MasterViewModel MasterViewModel =>
            _masterViewModel ?? (_masterViewModel = new MasterViewModel());

        private SavedViewModel _savedViewModel;
        public SavedViewModel SavedViewModel =>
            _savedViewModel ?? (_savedViewModel = new SavedViewModel());

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
