using ProjektSemestralnyOOP.Commands;
using System;
using System.Windows.Input;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using ProjektSemestralnyOOP.DBcontext;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private string _login;

        public string Login
        {
            get { return _login; }
            set
            { 
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public LoginWindow Window { get; }

        public ICommand SubmitButton { get; }
        public ICommand CancelButton { get; }

        public LoginViewModel(LoginWindow window)
        {
            SubmitButton = new RelayCommand(SubmitCommand);
            CancelButton = new RelayCommand(CancelCommand);
            Window = window;
        }

        private void CancelCommand()
        {
            Window.Close();
        }

        private async void SubmitCommand()
        {
            UserService service = new(new RacingDBContextFactory());
            bool isLogged = await service.LoginUserAsync(Login, Password);

            if (isLogged) 
            { 
                MessageBox.Show("True", "islogged?");
                Window.Close();
                return;
            }

            MessageBox.Show("False", "islogged?");
        }
    }
}
