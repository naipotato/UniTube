using GalaSoft.MvvmLight.Command;

using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using UniTube.Services;
using UniTube.Core.Authentication;
using UniTube.Dialogs;
using UniTube.Helpers;

using Windows.Storage;

namespace UniTube.ViewModels
{
    public class WelcomeViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;

        private ApplicationDataContainer _roamingSettings;

        public WelcomeViewModel()
        {
            _roamingSettings = ApplicationData.Current.RoamingSettings;
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ContinueCommand
        {
            get { return new RelayCommand(Continue); }
        }

        public ICommand LogInCommand
        {
            get { return new RelayCommand(LogIn); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Continue()
        {
            await _roamingSettings.SaveAsync("Initialized", true);
            NavigationService.SetMainPage("MasterPage");
        }

        private async void LogIn()
        {
            IsLoading = true;
            var authResponse = await Authenticator.AuthenticateAsync(new UserCancelDialog(), new ErrorHttpDialog());
            IsLoading = false;

            if (authResponse != null)
            {
                App.AuthInfo = authResponse;
                await _roamingSettings.SaveAsync("RefreshToken", authResponse.Refresh_token);
                await _roamingSettings.SaveAsync("Initialized", true);
                NavigationService.SetMainPage("MasterPage");
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
