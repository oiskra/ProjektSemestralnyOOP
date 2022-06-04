using ProjektSemestralnyOOP.Commands;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    /// <summary>
    /// Provides interaction logic for Races view.
    /// </summary>
    public class RacesViewModel : BaseViewModel
    {
        private User _loggedUser;
        private IRaceService _raceService;

        private ObservableCollection<Race> _races;
        /// <summary>
        /// Provides collection of races from database.
        /// </summary>
        public ObservableCollection<Race> Races 
        {
            get => _races;
            
            set
            {
                _races = value;
                OnPropertyChanged(nameof(Races));
            }
        }

        /// <summary>
        /// Provides a command for displaying all races in view.
        /// </summary>
        public ICommand SeeAllButton { get; }
        /// <summary>
        /// Provides a command for displaying logged user races in view.
        /// </summary>
        public ICommand SeeYoursButton { get; }

        /// <summary>
        /// Initializes a new instance of the RacesViewModel class
        /// </summary>
        /// <param name="loggedUser">Object of logged in user</param>
        public RacesViewModel(User loggedUser)
        {
            _raceService = new RaceService(new RacingDBContextFactory());
            SeeAllButton = new RelayCommand(SeeAllCommand);
            SeeYoursButton = new RelayCommand(SeeYoursCommand);
            _loggedUser = loggedUser;
        }

        private async void SeeYoursCommand() 
            => Races = new ObservableCollection<Race>(await _raceService.ReadRaceAsync(_loggedUser.Id));

        private async void SeeAllCommand() 
            => Races = new ObservableCollection<Race>(await _raceService.ReadAllRacesAsync());
    }
}
