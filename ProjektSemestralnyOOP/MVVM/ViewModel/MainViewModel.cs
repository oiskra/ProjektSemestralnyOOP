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
        private readonly ICarService _carService = new CarService(new RacingDBContextFactory());
        private readonly ViewModelMediator _mediator;
        private StartUpViewModel _startUpVM;
        private ProfileViewModel _profileVM;
        private YourCarsViewModel _yourCarsVM;
        private MarketViewModel _marketVM;
        private RacesViewModel _racesVM;
        private AdminPanelViewModel _adminPanelVM;

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

        private bool _showButton;
        public bool ShowButton
        {
            get => _showButton; 
            set
            {
                _showButton = value;
                OnPropertyChanged(nameof(ShowButton));
            }
        }

        public ICommand ProfileButton { get; }
        public ICommand MarketButton { get; }
        public ICommand YourCarsButton { get; }
        public ICommand RacesButton { get; }
        public ICommand AdminPanelButton { get; }
        public ICommand LogOutButton { get; }

        public MainViewModel(ViewModelMediator mediator)
        {
            ProfileButton = new RelayCommand(ProfileNavCommand, x => _loggedUser != null);
            MarketButton = new RelayCommand(MarketNavCommand, x => _loggedUser != null);
            YourCarsButton = new RelayCommand(YourCarsNavCommand, x => _loggedUser != null);
            RacesButton = new RelayCommand(RacesNavCommand, x => _loggedUser != null);
            LogOutButton = new RelayCommand(LogOutCommand, x => _loggedUser != null);
            AdminPanelButton = new RelayCommand(AdminPanelNavCommand);

            _mediator = mediator;
            _mediator.UserLogged += OnUserLogged;
            InitiateStartupView();
        }

        private void LogOutCommand()
        {
            _loggedUser = null;
            _mediator.LogOutUser();
            ShowButton = false;    
            CurrentView = _startUpVM;
        }
        private void AdminPanelNavCommand()
            => CurrentView = _adminPanelVM;

        private void RacesNavCommand()
            => CurrentView = _racesVM;

        private void YourCarsNavCommand()
        {
            List<Car> list = _carService.ReadCarsAsync(_loggedUser.Id);
            _mediator.UpdateYourCars(list);
            CurrentView = _yourCarsVM;
        }

        private void MarketNavCommand()
        {
            List<Car> list = _carService.ReadMarketAsync();
            _mediator.UpdateMarket(list);
            CurrentView = _marketVM;
        }

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
            ShowButton = _loggedUser.Username == "admin" && _loggedUser.Login == "admin";
            if (ShowButton) 
                _adminPanelVM = new AdminPanelViewModel(_loggedUser);
            _marketVM = new MarketViewModel(_loggedUser, _mediator);
            _profileVM = new ProfileViewModel(_loggedUser);
            _yourCarsVM = new YourCarsViewModel(_loggedUser, _mediator);
            _racesVM = new RacesViewModel(_loggedUser);
        }
    }
}
