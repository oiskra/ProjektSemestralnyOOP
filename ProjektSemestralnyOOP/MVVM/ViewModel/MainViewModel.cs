using ProjektSemestralnyOOP.Commands;
using System;
using System.ComponentModel;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using System.Windows;
using ProjektSemestralnyOOP.MVVM.Model;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private object _currentView;
        private User _loggedUser;
        private readonly ViewModelMediator _mediator;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public StartUpViewModel StartUpVM { get; }

        public MainViewModel(ViewModelMediator mediator)
        {
            LoginWindow loginWin = new();
            RegisterWindow registerWin = new();

            loginWin.DataContext = new LoginViewModel(loginWin, mediator);
            registerWin.DataContext = new RegisterViewModel(registerWin);


            StartUpVM = new StartUpViewModel(loginWin, registerWin);
            _currentView = StartUpVM;
            _mediator = mediator;

            _mediator.UserLogged += OnUserLogged;
        }

        private void OnUserLogged(User obj)
        {
            _loggedUser = obj;
            MessageBox.Show(_loggedUser.Username, "test");
        }
    }
}
