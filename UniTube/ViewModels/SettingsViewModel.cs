using System;

using Template10.Mvvm;

using UniTube.Services.SettingsServices;

using Windows.System;
using Windows.UI.Xaml;

namespace UniTube.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private DelegateCommand<string> _changeThemeCommand;
        private bool _isThemeChanged;
        private DelegateCommand<string> _launchUriCommand;
        private SettingsService _settings;

        public SettingsViewModel()
        {
            _settings = SettingsService.Instance;
        }

        public ElementTheme AppTheme
        {
            get => _settings.AppTheme;
            set
            {
                if (_settings.AppTheme != value)
                {
                    _settings.AppTheme = value;
                    IsThemeChanged = true;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsThemeChanged
        {
            get => _isThemeChanged;
            set => Set(ref _isThemeChanged, value);
        }

        public DelegateCommand<string> ChangeThemeCommand
            => _changeThemeCommand ?? (_changeThemeCommand = new DelegateCommand<string>(ChangeTheme));

        public DelegateCommand<string> LaunchUriCommand
            => _launchUriCommand ?? (_launchUriCommand = new DelegateCommand<string>(LaunchUri));

        private void ChangeTheme(string obj)
        {
            Enum.TryParse(obj, out ElementTheme theme);
            AppTheme = theme;
        }

        private async void LaunchUri(string obj) => await Launcher.LaunchUriAsync(new Uri(obj));
    }
}
