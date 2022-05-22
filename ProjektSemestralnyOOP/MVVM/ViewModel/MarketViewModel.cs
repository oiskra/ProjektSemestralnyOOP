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
    public class MarketViewModel : BaseViewModel
    {
        private readonly ICarService _service = new CarService(new RacingDBContextFactory());
        private readonly User _loggedUser;
        private readonly ViewModelMediator _mediator;
        
        private ObservableCollection<Tuple<Car, Statistic>> _marketCars;
        public ObservableCollection<Tuple<Car, Statistic>> MarketCars
        {
            get => _marketCars;
            set
            {
                _marketCars = value;
                OnPropertyChanged(nameof(MarketCars));
            }
        }

        public Tuple<Car, Statistic> SelectedCar { get; set; }

        public ICommand BuyButton { get; }
        public ICommand SellButton { get; }

        public MarketViewModel(User user, ViewModelMediator mediator)
        {
            BuyButton = new RelayCommand(BuyCommand);
            SellButton = new RelayCommand(SellCommand);
            _mediator = mediator;
            _mediator.MarketUpdated += OnMarketUpdated;
            _loggedUser = user;
        }

        private void OnMarketUpdated(List<Tuple<Car, Statistic>> obj)
            => AssignListFromDb();

        private async void BuyCommand()
        {
            if(SelectedCar is null)
            {
                MessageBox.Show("Please select a car", "Info");
                return;
            }
            await _service.BuyCarAsync(SelectedCar.Item1.Id, _loggedUser.Id);
            AssignListFromDb();
        }

        private async void SellCommand()
        {
            if (SelectedCar is null)
            {
                MessageBox.Show("Please select a car", "Info");
                return;
            }

            if (SelectedCar.Item1.UserId != _loggedUser.Id) 
            {
                MessageBox.Show("You can\'t sell a car you don\'t own", "Info");
                return;
            }

            await _service.SellCarAsync(SelectedCar.Item1.Id, _loggedUser.Id);
            AssignListFromDb();
        }

        private async void AssignListFromDb()
            => MarketCars = new ObservableCollection<Tuple<Car, Statistic>>(await _service.ReadMarketAsync()); 
    }
}
