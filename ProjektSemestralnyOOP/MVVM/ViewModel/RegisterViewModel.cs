using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for Register view.
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {
        private readonly RegisterWindow _window;
        private readonly IUserService _userService = new UserService(new RacingDBContextFactory());


        private string _username;
        /// <summary>
        /// Contains username component for Register Form.
        /// </summary>
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
        /// <summary>
        /// Contains login component for Register Form.
        /// </summary>
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        /// <summary>
        /// Contains password component for Register Form.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        /// <summary>
        /// Provides a command for submitting register form.
        /// </summary>
        public ICommand SubmitCommand { get; }
        /// <summary>
        /// Provides a command for canceling login form.
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Initializes a new instance of the RegisterViewModel class
        /// </summary>
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
            User newUser = new()
            {
                Username = Username,
                Login = Login,
                Password = Password,
                Money = 1000
            };
            
            await _userService.RegisterUserAsync(newUser);
            _window.Hide();
            Username = null;
            Login = null;
            Password = null;
        }
    }
}
