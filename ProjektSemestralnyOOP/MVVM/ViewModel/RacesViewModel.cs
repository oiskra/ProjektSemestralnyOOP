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
    public class RacesViewModel : BaseViewModel
    {
        private User _loggedUser;
        private IRaceService _raceService;

        private ObservableCollection<Race> _races;
        public ObservableCollection<Race> Races 
        {
            get => _races;
            
            set
            {
                _races = value;
                OnPropertyChanged(nameof(Races));
            }
        }

        public ICommand SeeAllButton { get; }
        public ICommand SeeYoursButton { get; }

        public RacesViewModel(User user)
        {
            _raceService = new RaceService(new RacingDBContextFactory());
            SeeAllButton = new RelayCommand(SeeAllCommand);
            SeeYoursButton = new RelayCommand(SeeYoursCommand);
            _loggedUser = user;
        }

        private async void SeeYoursCommand()
        {
            Races = new ObservableCollection<Race>(await _raceService.ReadRaceAsync(_loggedUser.Id));
        }

        private async void SeeAllCommand()
        {
            Races = new ObservableCollection<Race>(await _raceService.ReadAllRacesAsync());
        }
    }
}
