using ProjektSemestralnyOOP.Commands;
using System.Windows;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class StartUpViewModel 
    {
        public ICommand LoginButton { get; }
        public ICommand RegisterButton { get; }

        public StartUpViewModel()
        {
            LoginButton = new RelayCommand(LoginCommand);
            RegisterButton = new RelayCommand(RegisterCommand);
        }

        private void RegisterCommand()
        {
            Window window = new RegisterWindow();
            window.ShowDialog();
        }

        private void LoginCommand()
        {
            Window window = new LoginWindow();
            window.ShowDialog();
        }
    }
}
