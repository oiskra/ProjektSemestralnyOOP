using ProjektSemestralnyOOP.Commands;
using System;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using ProjektSemestralnyOOP.MVVM.Model;
using System.Windows.Input;
using System.Collections.Generic;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for MainWindow view
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private User _loggedUser;
        private readonly ICarService _carService = new CarService(new());
        private readonly ViewModelMediator _mediator;
        private StartUpViewModel _startUpVM;
        private ProfileViewModel _profileVM;
        private YourCarsViewModel _yourCarsVM;
        private MarketViewModel _marketVM;
        private RacesViewModel _racesVM;
        private AdminPanelViewModel _adminPanelVM;

        private object _currentView;
        /// <summary>
        /// Determines the current view for the application.
        /// </summary>
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        private bool _showAdminButton;
        /// <summary>
        /// Determines whether the admin panel button should be shown or not.
        /// </summary>
        public bool ShowAdminButton
        {
            get => _showAdminButton; 
            set
            {
                _showAdminButton = value;
                OnPropertyChanged(nameof(ShowAdminButton));
            }
        }

        /// <summary>
        /// Provides a navigation command to Profile view.
        /// </summary>
        public ICommand ProfileButton { get; }
        /// <summary>
        /// Provides a navigation command to Market view.
        /// </summary>
        public ICommand MarketButton { get; }
        /// <summary>
        /// Provides a navigation command to YourCars view.
        /// </summary>
        public ICommand YourCarsButton { get; }
        /// <summary>
        /// Provides a navigation command to Races view.
        /// </summary>
        public ICommand RacesButton { get; }
        /// <summary>
        /// Provides a navigation command to AdminPanel view.
        /// </summary>
        public ICommand AdminPanelButton { get; }
        /// <summary>
        /// Provides a command for user to log out
        /// </summary>
        public ICommand LogOutButton { get; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class
        /// </summary>
        public MainViewModel(ViewModelMediator mediator)
        {
            ProfileButton = new RelayCommand(ProfileNavCommand, () => _loggedUser != null);
            MarketButton = new RelayCommand(MarketNavCommand, () => _loggedUser != null);
            YourCarsButton = new RelayCommand(YourCarsNavCommand, () => _loggedUser != null);
            RacesButton = new RelayCommand(RacesNavCommand, () => _loggedUser != null);
            LogOutButton = new RelayCommand(LogOutCommand, () => _loggedUser != null);
            AdminPanelButton = new RelayCommand(AdminPanelNavCommand);
            _mediator = mediator;
            _mediator.UserLogged += OnUserLogged;
            InitiateStartupView();
        }

        private void LogOutCommand()
        {
            _loggedUser = null;
            _mediator.LogOutUser();
            ShowAdminButton = false;    
            CurrentView = _startUpVM;
        }
        private void AdminPanelNavCommand() 
            => CurrentView = _adminPanelVM;

        private void RacesNavCommand()
            => CurrentView = _racesVM;

        private async void YourCarsNavCommand()
        {
            List<Tuple<Car, Statistic>> list = await _carService.ReadCarsAsync(_loggedUser.Id);
            _mediator.UpdateYourCars(list);
            CurrentView = _yourCarsVM;
        }

        private async void MarketNavCommand()
        {
            List<Tuple<Car, Statistic>> list = await _carService.ReadMarketAsync();
            _mediator.UpdateMarket(list);
            CurrentView = _marketVM;
        }

        private void ProfileNavCommand()
        {
            _mediator.UpdateProfileInfo();
            CurrentView = _profileVM;
        }

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
            ShowAdminButton = _loggedUser.Username == "admin" && _loggedUser.Login == "admin";
            if (ShowAdminButton) 
                _adminPanelVM = new AdminPanelViewModel(_loggedUser);
            _marketVM = new MarketViewModel(_loggedUser, _mediator);
            _profileVM = new ProfileViewModel(_loggedUser, _mediator);
            _yourCarsVM = new YourCarsViewModel(_loggedUser, _mediator);
            _racesVM = new RacesViewModel(_loggedUser);
        }
    }
}
