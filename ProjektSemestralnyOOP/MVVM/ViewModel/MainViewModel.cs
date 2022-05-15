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
        private YourCarsViewModel _yourCarsVm;
        private MarketViewModel _marketVM;

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
            MarketButton = new RelayCommand(MarketNavCommand);
            YourCarsButton = new RelayCommand(YourCarsNavCommand);
            ChallegeButton = new RelayCommand(ChallengeNavCommand);
            YourRacesButton = new RelayCommand(YourRacesNavCommand);


            _mediator = mediator;
            _mediator.UserLogged += OnUserLogged;
            InitiateStartupView();
        }

        private void YourRacesNavCommand()
            => CurrentView = _yourCarsVm;

        private void ChallengeNavCommand()
        {
            throw new NotImplementedException();
        }

        private void YourCarsNavCommand()
        {
            ICarService service = new CarService(new RacingDBContextFactory());
            List<Car> list = service.ReadCarsAsync(_loggedUser.Id);
            _mediator.UpdateYourCars(list);
            CurrentView = _yourCarsVm;
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

            _startUpVM = new StartUpViewModel(loginWin, registerWin);
            _currentView = _startUpVM;
        }

        private void OnUserLogged(User obj)
        {
            _loggedUser = obj;
            _marketVM = new MarketViewModel(_loggedUser);
            _profileVM = new ProfileViewModel(_loggedUser);
            _yourCarsVm = new YourCarsViewModel(_loggedUser, _mediator);
        }
    }
}
