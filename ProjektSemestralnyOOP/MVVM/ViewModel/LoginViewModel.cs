using ProjektSemestralnyOOP.Commands;
using System.Windows.Input;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using ProjektSemestralnyOOP.DBcontext;
using System.Windows;
using ProjektSemestralnyOOP.MVVM.Model;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for Login view.
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private readonly ViewModelMediator _mediator;
        private LoginWindow _window; 
        
        private string _login;
        /// <summary>
        /// Contains login component for Login Form.
        /// </summary>
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
        /// <summary>
        /// Contains password component for Login Form.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        /// <summary>
        /// Provides a command for submitting login form.
        /// </summary>
        public ICommand SubmitButton { get; }
        /// <summary>
        /// Provides a command for canceling login form.
        /// </summary>
        public ICommand CancelButton { get; }

        /// <summary>
        /// Initializes a new instance of the LoginViewModel class
        /// </summary>
        public LoginViewModel(LoginWindow window, ViewModelMediator mediator)
        {
            SubmitButton = new RelayCommand(SubmitLoginCommand, () => 
                !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password));
            CancelButton = new RelayCommand(CancelLoginCommand);
            
            _window = window;
            _mediator = mediator;
        }

        private void CancelLoginCommand()
        {
            _window.Hide();
            Password = null;
            Login = null;
        }

        private async void SubmitLoginCommand()
        {
            IUserService service = new UserService(new RacingDBContextFactory());
            User loggedUser = await service.LoginUserAsync(Login, Password);

            if (loggedUser is User)
            {
                _mediator.LoginUser(loggedUser);
                MessageBox.Show($"You logged in as {loggedUser.Username}", "Info");
                _window.Hide();
                Password = null;
                Login = null;
                return;
            }

            MessageBox.Show("Failed to login, try again", "Info");
        }
    }
}
