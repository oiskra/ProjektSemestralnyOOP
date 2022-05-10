using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        public object CurrentView { get; set; }


        public MainViewModel()
        {
            CurrentView = new StartUpViewModel();
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
