using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace UniTube.ViewModels
{
    public class MasterViewModel : INotifyPropertyChanged
    {
        private bool _isPaneOpen;

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

        public ICommand OpenClosePaneCommand
        {
            get { return new RelayCommand(OpenClosePane); }
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
