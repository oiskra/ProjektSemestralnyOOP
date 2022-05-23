using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System.Windows;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for Profile view.
    /// </summary>
    public class ProfileViewModel : BaseViewModel
    {
        private readonly ViewModelMediator _mediator; 
        private readonly User _loggedUser;

        private int _id;
        /// <summary>
        /// Contains ID component to display in view.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _username;
        /// <summary>
        /// Contains username component to display in view.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _login;
        /// <summary>
        /// Contains login component to display in view.
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
        /// Contains password component to display in view.
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

        private int _money;
        /// <summary>
        /// Contains money component to display in view.
        /// </summary>
        public int Money
        {
            get => _money;
            set
            {
                _money = value;
                OnPropertyChanged(nameof(Money));
            }
        }

        private string _usernameToUpdate;
        /// <summary>
        /// Contains updated username component.
        /// </summary>
        public string UsernameToUpdate
        {
            get { return _usernameToUpdate; }
            set
            {
                _usernameToUpdate = value;
                OnPropertyChanged(nameof(UsernameToUpdate));
            }
        }

        private string _loginToUpdate;
        /// <summary>
        /// Contains updated login component.
        /// </summary>
        public string LoginToUpdate
        {
            get { return _loginToUpdate; }
            set
            {
                _loginToUpdate = value;
                OnPropertyChanged(nameof(LoginToUpdate));
            }
        }

        private string _passwordToUpdate;
        /// <summary>
        /// Contains updated password component.
        /// </summary>
        public string PasswordToUpdate
        {
            get { return _passwordToUpdate; }
            set
            {
                _passwordToUpdate = value;
                OnPropertyChanged(nameof(PasswordToUpdate));
            }
        }

        /// <summary>
        /// Provides a command for updating user info
        /// </summary>
        public ICommand UpdateButton { get; }

        /// <summary>
        /// Initializes a new instance of the ProfileViewModel class
        /// </summary>
        /// <param name="loggedUser">Object of logged in user</param>
        public ProfileViewModel(User user, ViewModelMediator mediator)
        {
            _mediator = mediator;
            _mediator.ProfileInfoUpdated += OnProfileInfoUpdated;
            _loggedUser = user;

            _usernameToUpdate = _loggedUser.Username;
            _loginToUpdate = _loggedUser.Login;
            _passwordToUpdate = _loggedUser.Password;
            
            UpdateButton = new RelayCommand(UpdateCommand, () =>
                !string.IsNullOrEmpty(UsernameToUpdate) &&
                !string.IsNullOrEmpty(LoginToUpdate) &&
                !string.IsNullOrEmpty(PasswordToUpdate) &&
                (Username, Login, Password) != (UsernameToUpdate, LoginToUpdate, PasswordToUpdate));

        }

        private async void UpdateCommand()
        {
            if (_loggedUser.Username == "admin")
            {
                MessageBox.Show("You cannot update admin account", "Info");
                return;
            }

            User updatedUser = new()
            {
                Id = Id,
                Username = UsernameToUpdate,
                Login = LoginToUpdate,
                Password = PasswordToUpdate,
                Money = Money
            };

            IUserService service = new UserService(new());
            await service.UpdateUserAsync(updatedUser);

            Username = UsernameToUpdate;
            Login = LoginToUpdate;
            Password = PasswordToUpdate;
        }

        private async void OnProfileInfoUpdated()
        {
            IUserService userService = new UserService(new());
            User user = await userService.ReadUserAsync(_loggedUser.Id);
            Id = user.Id;
            Username = user.Username;
            Login = user.Login;
            Password = user.Password;
            Money = user.Money;
        }
    }
}
