using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Base class for all viewmodels
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
