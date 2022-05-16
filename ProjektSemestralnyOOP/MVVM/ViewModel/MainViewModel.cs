using ProjektSemestralnyOOP.Commands;
using System;
using System.ComponentModel;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using System.Windows;
using ProjektSemestralnyOOP.MVVM.Model;
using System.Windows.Input;
using ProjektSemestralnyOOP.DBcontext;
using System.Collections.Generic;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private User _loggedUser;
        private readonly ViewModelMediator _mediator;
        private StartUpViewModel _startUpVM;
        private ProfileViewModel _profileVM;
        private YourCarsViewModel _yourCarsVM;
        private MarketViewModel _marketVM;
        private RacesViewModel _racesVM;

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
        public ICommand RacesButton { get; }
        public ICommand LogOutButton { get; }

        public MainViewModel(ViewModelMediator mediator)
        {
            ProfileButton = new RelayCommand(ProfileNavCommand, x => _loggedUser != null);
            MarketButton = new RelayCommand(MarketNavCommand, x => _loggedUser != null);
            YourCarsButton = new RelayCommand(YourCarsNavCommand, x => _loggedUser != null);
            RacesButton = new RelayCommand(RacesNavCommand, x => _loggedUser != null);
            LogOutButton = new RelayCommand(LogOutCommand, x => _loggedUser != null);


            _mediator = mediator;
            _mediator.UserLogged += OnUserLogged;
            InitiateStartupView();
        }

        private void LogOutCommand()
        {
            _loggedUser = null;
            _mediator.LogOutUser();
            CurrentView = _startUpVM;
        }

        private void RacesNavCommand()
            => CurrentView = _racesVM;

        private void YourCarsNavCommand()
        {
            ICarService service = new CarService(new RacingDBContextFactory());
            List<Car> list = service.ReadCarsAsync(_loggedUser.Id);
            _mediator.UpdateYourCars(list);
            CurrentView = _yourCarsVM;
        }

        private void MarketNavCommand()
            => CurrentView = _marketVM;

        private void ProfileNavCommand()
            => CurrentView = _profileVM;

        private void InitiateStartupView()
        {
            LoginWindow loginWin = new();
            RegisterWindow registerWin = new();
            loginWin.DataContext = new LoginViewModel(loginWin, _mediator);
            registerWin.DataContext = new RegisterViewModel(registerWin);

            _startUpVM = new StartUpViewModel(loginWin, registerWin, _mediator);
            _currentView = _startUpVM;
        }

        private void OnUserLogged(User obj)
        {
            _loggedUser = obj;
            _marketVM = new MarketViewModel(_loggedUser);
            _profileVM = new ProfileViewModel(_loggedUser);
            _yourCarsVM = new YourCarsViewModel(_loggedUser, _mediator);
            _racesVM = new RacesViewModel(_loggedUser);
        }
    }
}
