using ProjektSemestralnyOOP.Commands;
using System.Windows;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class StartUpViewModel 
    {
        private readonly LoginWindow _loginWindow;
        private readonly RegisterWindow _registerWindow;

        public ICommand LoginButton { get; }
        public ICommand RegisterButton { get; }

        public StartUpViewModel(LoginWindow loginWindow, RegisterWindow registerWindow)
        {
            LoginButton = new RelayCommand(LoginCommand);
            RegisterButton = new RelayCommand(RegisterCommand);
            _loginWindow = loginWindow;
            _registerWindow = registerWindow;
        }

        private void RegisterCommand()
            => _registerWindow.Show();

        private void LoginCommand()
            => _loginWindow.Show();
    }
}
