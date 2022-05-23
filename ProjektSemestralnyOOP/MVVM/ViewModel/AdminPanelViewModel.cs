using ProjektSemestralnyOOP.Commands;
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
    /// Provides interaction logic for AdminPanel view.
    /// </summary>
    public class AdminPanelViewModel : BaseViewModel
    {
        private static RacingDBContextFactory _contextFactory = new();
        private readonly IUserService _userService = new UserService(_contextFactory);
        private readonly ICarService _carService = new CarService(_contextFactory);
        private User _loggedUser;       
        
        private ObservableCollection<User> _users;
        /// <summary>
        /// Provides collection of users from database.
        /// </summary>
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private string _idToDelete;
        /// <summary>
        /// Contains ID of a user that has to be deleted from the database.
        /// </summary>
        public string IdToDelete
        {
            get => _idToDelete;
            set
            {
                _idToDelete = value;
                OnPropertyChanged(nameof(IdToDelete));
            }
        }

        private string _brand;
        /// <summary>
        /// Contains brand component for new Car model.
        /// </summary>
        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged(nameof(Brand));
            }
        }

        private string _model;
        /// <summary>
        /// Contains model component for new Car model.
        /// </summary>
        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private string _speed;
        /// <summary>
        /// Contains speed component for new Car model.
        /// </summary>
        public string Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }

        private string _acceleration;
        /// <summary>
        /// Contains acceleration component for new Car model.
        /// </summary>
        public string Acceleration
        {
            get => _acceleration;
            set
            {
                _acceleration = value;
                OnPropertyChanged(nameof(Acceleration));
            }
        }

        private string _grip;
        /// <summary>
        /// Contains grip component for new Car model.
        /// </summary>
        public string Grip
        {
            get => _grip;
            set
            {
                _grip = value;
                OnPropertyChanged(nameof(Grip));
            }
        }

        private string _braking;
        /// <summary>
        /// Contains Braking component for new Car model.
        /// </summary>
        public string Braking
        {
            get => _braking;
            set
            {
                _braking = value;
                OnPropertyChanged(nameof(Braking));
            }
        }

        /// <summary>
        /// Provides a command for deleting user from the database.
        /// </summary>
        public ICommand DeleteUserButton { get; }
        /// <summary>
        /// Provides a command for inserting new Car into the database.
        /// </summary>
        public ICommand CreateCarButton { get; }

        /// <summary>
        /// Initializes a new instance of the AdminPanel class
        /// </summary>
        /// <param name="loggedUser">Object of logged in user</param>
        public AdminPanelViewModel(User loggedUser)
        {
            CreateCarButton = new RelayCommand(CreateCarCommand, () => 
                !string.IsNullOrEmpty(Brand) &&
                !string.IsNullOrEmpty(Model) &&
                !string.IsNullOrEmpty(Speed) &&
                !string.IsNullOrEmpty(Acceleration) &&
                !string.IsNullOrEmpty(Grip) &&
                !string.IsNullOrEmpty(Braking));
            DeleteUserButton = new RelayCommand(DeleteUserCommand, () => !string.IsNullOrEmpty(IdToDelete));
            _loggedUser = loggedUser;
            LoadUsersList();
        }

        private async void CreateCarCommand()
        {
            try
            {
                Statistic newStatistic = new()
                {
                    Speed = int.Parse(Speed),
                    Acceleration = int.Parse(Acceleration),
                    Grip = int.Parse(Grip),
                    Braking = int.Parse(Braking)
                };

                Car newCar = new()
                {
                    Brand = Brand,
                    Model = Model,
                    IsAvailable = true,
                    Price = (newStatistic.Braking + newStatistic.Grip + newStatistic.Acceleration + newStatistic.Speed) * 100,
                    UserId = null
                };

                newStatistic.CarId = newCar.Id;

                await _carService.CreateCarAsync(newCar);
                await _carService.AddStatistic(newStatistic);
                MessageBox.Show("Car created successfully", "Info");

            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong input values", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteUserCommand()
        {
            try
            {
                int id = int.Parse(IdToDelete);
                bool isDeleted = await _userService.DeleteUserAsync(id);
                if (isDeleted)
                {
                    MessageBox.Show("User deleted successfully", "Info");
                    LoadUsersList();
                    return;
                }
                MessageBox.Show("Failed to delete user", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong input values", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadUsersList()
        {
            List<User> usersList = await _userService.ReadAllAsync();
            Users = new ObservableCollection<User>(usersList);
        }
    }
}
