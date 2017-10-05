using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.Storage;

namespace UniTube.ViewModels
{
    public class MasterViewModel : INotifyPropertyChanged
    {
        private bool _isPaneOpen;
        private bool _isLoggedIn;

        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                if (_isPaneOpen != value)
                {
                    _isPaneOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand OpenClosePaneCommand
        {
            get { return new RelayCommand(OpenClosePane); }
        }

        public MasterViewModel()
        {
            IsLoggedIn = ApplicationData.Current.RoamingSettings.Values.ContainsKey("RefreshToken");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OpenClosePane()
        {
            IsPaneOpen = !IsPaneOpen;
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
