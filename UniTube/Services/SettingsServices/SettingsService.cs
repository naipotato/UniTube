using Template10.Services.SettingsService;

using Windows.UI.Xaml;

namespace UniTube.Services.SettingsServices
{
    public class SettingsService
    {
        ISettingsHelper _helper;

        public static SettingsService Instance => new SettingsService();

        private SettingsService() => _helper = new SettingsHelper();

        public ElementTheme AppTheme
        {
            get => _helper.Read(nameof(AppTheme), ElementTheme.Default);
            set => _helper.Write(nameof(AppTheme), value);
        }
    }
}
