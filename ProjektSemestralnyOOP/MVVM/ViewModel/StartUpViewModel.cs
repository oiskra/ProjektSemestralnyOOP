using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for StartUp view.
    /// </summary>
    public class StartUpViewModel : BaseViewModel
    {
        private readonly LoginWindow _loginWindow;
        private readonly RegisterWindow _registerWindow;
        private ViewModelMediator _mediator;
        private User _loggedUser;

        /// <summary>
        /// Provides a command for displaying Login form window.
        /// </summary>
        public ICommand LoginButton { get; }
        /// <summary>
        /// Provides a command for displaying Register form window.
        /// </summary>
        public ICommand RegisterButton { get; }

        /// <summary>
        /// Initializes a new instance of the StartUpViewModel class
        /// </summary>
        public StartUpViewModel(LoginWindow loginWindow, RegisterWindow registerWindow, ViewModelMediator mediator)
        {
            _mediator = mediator;
            _mediator.UserLogged += OnUserLogged;
            _mediator.UserLoggedOut += OnUserLoggedOut;
            LoginButton = new RelayCommand(LoginCommand, () => _loggedUser is null);
            RegisterButton = new RelayCommand(RegisterCommand, () => _loggedUser is null);
            _loginWindow = loginWindow;
            _registerWindow = registerWindow;
        }

        private void OnUserLoggedOut()
            => _loggedUser = null;

        private void OnUserLogged(User obj)
            => _loggedUser = obj;

        private void RegisterCommand()
            => _registerWindow.Show();

        private void LoginCommand()
            => _loginWindow.Show();
    }
}
