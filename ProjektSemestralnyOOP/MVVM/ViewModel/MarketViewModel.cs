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
    public class MarketViewModel : BaseViewModel
    {
        private readonly ICarService _service = new CarService(new RacingDBContextFactory());
        private readonly User _loggedUser;
        
        private ObservableCollection<Car> _marketCars;
        public ObservableCollection<Car> MarketCars
        {
            get => _marketCars;
            set
            {
                _marketCars = value;
                OnPropertyChanged(nameof(MarketCars));
            }
        }

        public Car SelectedCar { get; set; }

        public ICommand BuyButton { get; }
        public ICommand SellButton { get; }

        public MarketViewModel(User user)
        {
            BuyButton = new RelayCommand(BuyCommand);
            SellButton = new RelayCommand(SellCommand);
            AssignListFromDb();
            _loggedUser = user;
        }

        private async void BuyCommand()
        {
            if(SelectedCar is null)
            {
                MessageBox.Show("Please select a car");
                return;
            }
            await _service.BuyCarAsync(SelectedCar.Id, _loggedUser.Id);
            AssignListFromDb();
        }

        private async void SellCommand()
        {
            if (SelectedCar is null)
            {
                MessageBox.Show("Please select a car");
                return;
            }
            await _service.SellCarAsync(SelectedCar.Id, _loggedUser.Id);
            AssignListFromDb();
        }

        private void AssignListFromDb()
        {
            MarketCars = new ObservableCollection<Car>(_service.ReadMarketAsync());
        }

    }
}
