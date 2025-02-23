﻿using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for YourCars view.
    /// </summary>
    public class YourCarsViewModel : BaseViewModel
    {
        private readonly static RacingDBContextFactory _contextFactory = new();
        private readonly IRaceService _raceService = new RaceService(_contextFactory);
        private readonly ViewModelMediator _mediatior;
        private readonly User _loggedUser;

        private ObservableCollection<Tuple<Car, Statistic>> _yourCars;
        /// <summary>
        /// Provides collection of logged user's cars from database.
        /// </summary>
        public ObservableCollection<Tuple<Car, Statistic>> YourCars
        {
            get => _yourCars;
            set
            {
                _yourCars = value;
                OnPropertyChanged(nameof(YourCars));
            }
        }
        private Tuple<Car, Statistic> _selectedCar;
        /// <summary>
        /// Contains selected object from the YourCars collection
        /// </summary>
        public Tuple<Car, Statistic> SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private string _challengedUsername;
        /// <summary>
        /// Contains username of a user that logged user is challenging.
        /// </summary>
        public string ChallengedUsername
        {
            get => _challengedUsername; 
            set 
            {
                _challengedUsername = value;
                OnPropertyChanged(nameof(ChallengedUsername));
            }
        }

        /// <summary>
        /// Provides a command for inserting new Race into the database.
        /// </summary>
        public ICommand ChallengeButton { get; set; }

        /// <summary>
        /// Initializes a new instance of the YourCarsViewModel class
        /// </summary>
        /// <param name="loggedUser">Object of logged in user</param>
        public YourCarsViewModel(User loggedUser, ViewModelMediator mediator)
        {
            ChallengeButton = new RelayCommand(ChallengeCommand, () => ChallengedUsername != null);
            _mediatior = mediator;
            _loggedUser = loggedUser;
            _mediatior.YourCarsUpdated += OnYourCarsUpdated;
        }

        private void OnYourCarsUpdated(List<Tuple<Car, Statistic>> obj)
        {
            YourCars = new ObservableCollection<Tuple<Car, Statistic>>(obj);
            SelectedCar = null;
        }

        private async void ChallengeCommand()
        {
            if(SelectedCar is null)
            {
                MessageBox.Show("You have to select a car you want to race with.", "Info");
                return;
            }
            await _raceService.CreateRaceAsync(_loggedUser.Username, ChallengedUsername, SelectedCar);
        }
    }
}
