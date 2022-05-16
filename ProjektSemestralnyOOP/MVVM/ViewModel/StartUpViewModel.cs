using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class StartUpViewModel 
    {
        private readonly LoginWindow _loginWindow;
        private readonly RegisterWindow _registerWindow;
        private ViewModelMediator _mediator;
        private User _loggedUser;

        public ICommand LoginButton { get; }
        public ICommand RegisterButton { get; }

        public StartUpViewModel(LoginWindow loginWindow, RegisterWindow registerWindow, ViewModelMediator mediator)
        {
            _mediator = mediator;
            _mediator.UserLogged += OnUserLogged;
            _mediator.UserLoggedOut += OnUserLoggedOut;
            LoginButton = new RelayCommand(LoginCommand, x => _loggedUser is null);
            RegisterButton = new RelayCommand(RegisterCommand, x => _loggedUser is null);
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
