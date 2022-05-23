using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class RegisterViewModel : BaseViewModel
    {
        private RegisterWindow _window;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public RegisterViewModel(RegisterWindow window)
        {
            SubmitCommand = new RelayCommand(SubmitRegisterCommand, () =>
                !string.IsNullOrEmpty(Username) &&
                !string.IsNullOrEmpty(Login) &&
                !string.IsNullOrEmpty(Password));
            CancelCommand = new RelayCommand(CancelRegisterCommand);
            _window = window;
        }

        private void CancelRegisterCommand()
        {
            _window.Hide();
            Username = null;
            Login = null;
            Password = null;
        }

        private async void SubmitRegisterCommand()
        {
            IUserService service = new UserService(new RacingDBContextFactory());
            User newUser = new()
            {
                Username = Username,
                Login = Login,
                Password = Password,
                Money = 1000
            };
            
            await service.RegisterUserAsync(newUser);
            _window.Hide();
            Username = null;
            Login = null;
            Password = null;
        }
    }
}
