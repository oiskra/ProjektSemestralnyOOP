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
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class YourCarsViewModel : BaseViewModel
    {
        private readonly static RacingDBContextFactory _contextFactory = new();
        private readonly ICarService _carService = new CarService(_contextFactory);
        private readonly IRaceService _raceService = new RaceService(_contextFactory);
        private ViewModelMediator _mediatior;
        private User _loggedUser;

        private ObservableCollection<Car> _yourCars;
        public ObservableCollection<Car> YourCars
        {
            get => _yourCars;
            set
            {
                _yourCars = value;
                OnPropertyChanged(nameof(YourCars));
            }
        }
        private Car _selectedCar;
        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        private string _challengedUsername;

        public string ChallengedUsername
        {
            get => _challengedUsername; 
            set 
            {
                _challengedUsername = value;
                OnPropertyChanged(nameof(ChallengedUsername));
            }
        }

        public ICommand ChallegeButton { get; set; }

        public YourCarsViewModel(User user, ViewModelMediator mediator)
        {
            _mediatior = mediator;
            _loggedUser = user;
            _mediatior.YourCarsUpdated += OnYourCarsUpdated;
            ChallegeButton = new RelayCommand(ChallegeCommand);
        }

        private void OnYourCarsUpdated(List<Car> obj)
        {
            YourCars = new ObservableCollection<Car>(obj);
            SelectedCar = null;
        }

        private void ChallegeCommand()
        {
            _raceService.CreateRaceAsync(_loggedUser.Username, ChallengedUsername, SelectedCar);
        }
    }
}
