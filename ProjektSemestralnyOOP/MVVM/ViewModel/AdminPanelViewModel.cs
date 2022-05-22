using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class AdminPanelViewModel : BaseViewModel
    {
        private static readonly RacingDBContextFactory _contextFactory = new();
        private readonly IUserService _userService = new UserService(_contextFactory);
        private readonly ICarService _carService = new CarService(_contextFactory);
        private User _loggedUser;
        


        private ObservableCollection<User> _users;
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
        public string Braking
        {
            get => _braking;
            set
            {
                _braking = value;
                OnPropertyChanged(nameof(Braking));
            }
        }

        public ICommand DeleteUserButton { get; }
        public ICommand CreateCarButton { get; }

        public AdminPanelViewModel(User user)
        {
            CreateCarButton = new RelayCommand(CreateCarCommand, x => !string.IsNullOrEmpty(Brand) &&
                                                                        !string.IsNullOrEmpty(Model) &&
                                                                        !string.IsNullOrEmpty(Speed) &&
                                                                        !string.IsNullOrEmpty(Acceleration) &&
                                                                        !string.IsNullOrEmpty(Grip) &&
                                                                        !string.IsNullOrEmpty(Braking));
            DeleteUserButton = new RelayCommand(DeleteUserCommand, x => !string.IsNullOrEmpty(IdToDelete));
            _loggedUser = user;
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
                newStatistic.Car = newCar;

                await _carService.CreateCarAsync(newCar);
                await _carService.AddStatistic(newStatistic);
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
