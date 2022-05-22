using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Provides methods that operate with database and Race entity 
    /// </summary>
    public class RaceService : IRaceService
    {
        private readonly RacingDBContext _context;
        private readonly ICarService _carService;

        public RaceService(RacingDBContextFactory context)
        {
            _context = context.CreateDbContext();
            _carService = new CarService(context);
        }

        public async Task CreateRaceAsync(string loggedUsername, string challengedUsername, Tuple<Car, Statistic> loggedUserCar)
        {
            User loggedUser = await _context.Users.Where(x => x.Username == loggedUsername).FirstOrDefaultAsync();
            User challengedUser = await _context.Users.Where(x => x.Username == challengedUsername).FirstOrDefaultAsync();
            List<Tuple<Car, Statistic>> challengedCarList = await _carService.ReadCarsAsync(challengedUser.Id);

            if (challengedCarList.Count == 0)
            {
                MessageBox.Show("Challenged user doesn\'t have any cars", "Info");
                return;
            }
            int rand = new Random().Next(0, challengedCarList.Count);
            Tuple<Car, Statistic> challengedCar = challengedCarList[rand];

            int statsOne = loggedUserCar.Item2.Speed + 
                           loggedUserCar.Item2.Acceleration +
                           loggedUserCar.Item2.Grip +
                           loggedUserCar.Item2.Braking;
            
            int statsTwo = challengedCar.Item2.Acceleration + 
                           challengedCar.Item2.Braking +
                           challengedCar.Item2.Grip +
                           challengedCar.Item2.Speed;

            string winner;
            if (statsOne == statsTwo)
            {
                winner = "draw";
                loggedUser.Money += 300;
                challengedUser.Money += 300;
            }
            else
            {
                winner = statsOne > statsTwo ? loggedUsername : challengedUsername;
                if (winner == loggedUsername)
                {
                    loggedUser.Money += 500;
                    challengedUser.Money += 100;
                }
                else
                {
                    challengedUser.Money += 500;
                    loggedUser.Money += 100;
                }
            }

            Race newRace = new()
            {
                RacerOne = loggedUsername,
                RacerTwo = challengedUsername,
                CarOne = $"{loggedUserCar.Item1.Brand} {loggedUserCar.Item1.Model}",
                CarTwo = $"{challengedCar.Item1.Brand} {challengedCar.Item1.Model}",
                Winner = winner
            };

            await _context.Races.AddAsync(newRace);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Race>> ReadAllRacesAsync()
        {
            List<Race> races = await _context.Races.ToListAsync();
            return races;
        }

        public async Task<List<Race>> ReadRaceAsync(int id)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == id);
            var userRaces = await  _context.Races.Where(x => x.RacerOne == user.Username || x.RacerTwo == user.Username).ToListAsync();
            return userRaces;
        }
    }
}
