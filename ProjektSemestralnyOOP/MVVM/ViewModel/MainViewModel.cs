using ProjektSemestralnyOOP.Commands;
using System;
using System.ComponentModel;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using System.Windows;
using ProjektSemestralnyOOP.MVVM.Model;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private User _loggedUser;
        private ViewModelMediator _mediator;
        private StartUpViewModel _startUpVM;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand ProfileButton { get; }
        public ICommand MarketButton { get; }
        public ICommand YourCarsButton { get; }
        public ICommand ChallegeButton { get; }
        public ICommand YourRacesButton { get; }

        public MainViewModel(ViewModelMediator mediator)
        {
            ProfileButton = new RelayCommand(ProfileNavCommand);
            MarketButton = new RelayCommand(MarketeNavCommand);
            YourCarsButton = new RelayCommand(YourCarsNavCommand);
            ChallegeButton = new RelayCommand(ChallengeNavCommand);
            YourRacesButton = new RelayCommand(YourRacesNavCommand);



            InitiateStartupView(mediator);
            _mediator.UserLogged += OnUserLogged;
        }

        private void YourRacesNavCommand()
        {
            
        }

        private void ChallengeNavCommand()
        {
            throw new NotImplementedException();
        }

        private void YourCarsNavCommand()
        {
            throw new NotImplementedException();
        }

        private void MarketeNavCommand()
        {
            throw new NotImplementedException();
        }

        private void ProfileNavCommand()
        {
            throw new NotImplementedException();
        }

        private void InitiateStartupView(ViewModelMediator mediator)
        {
            LoginWindow loginWin = new();
            RegisterWindow registerWin = new();
            loginWin.DataContext = new LoginViewModel(loginWin, mediator);
            registerWin.DataContext = new RegisterViewModel(registerWin);

            _startUpVM = new StartUpViewModel(loginWin, registerWin);
            _currentView = _startUpVM;
            _mediator = mediator;
        }

        private void OnUserLogged(User obj)
        {
            _loggedUser = obj;
            MessageBox.Show(_loggedUser.Username, "test");
        }
    }
}
