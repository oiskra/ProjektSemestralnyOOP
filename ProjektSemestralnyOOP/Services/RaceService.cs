using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Provides methods that operate with database and Race entity 
    /// </summary>
    public class RaceService : IRaceService
    {
        private readonly RacingDBContext _context;

        public RaceService(RacingDBContextFactory context)
        {
            _context = context.CreateDbContext();
        }

        public async Task CreateRaceAsync(string loggedUsername, string challengedUsername, Car loggedUserCar)
        {
            User loggedUser = await _context.Users.Where(x => x.Username == loggedUsername).FirstOrDefaultAsync();
            User challengedUser = await _context.Users.Where(x => x.Username == challengedUsername).FirstOrDefaultAsync();
            List<Car> challengedCarList = await _context.Market
                .Where(x => x.UserId == challengedUser.Id)
                .ToListAsync();
            
            int rand = new Random().Next(0, challengedCarList.Count);
            var challengedCarStats = challengedCarList[rand].Statistics;

            int statsOne = loggedUserCar.Statistics.Speed + 
                        loggedUserCar.Statistics.Acceleration + 
                        loggedUserCar.Statistics.Grip + 
                        loggedUserCar.Statistics.Braking;
            
            int statsTwo = challengedCarStats.Acceleration + 
                        challengedCarStats.Braking + 
                        challengedCarStats.Grip + 
                        challengedCarStats.Speed;

            string winner;
            if (statsOne == statsTwo)
            {
                winner = "draw";
                loggedUser.Money += 250;
                challengedUser.Money += 250;
            }
            else
            {
                winner = statsOne > statsTwo ? loggedUsername : challengedUsername;
                if (winner == loggedUsername)
                    loggedUser.Money += 500;
                else
                    challengedUser.Money += 500;
            }

            Race newRace = new Race
            {
                RacerOne = loggedUsername,
                RacerTwo = challengedUsername,
                CarOne = $"{loggedUserCar.Brand} {loggedUserCar.Model}",
                CarTwo = $"",
                Winner = winner
            };

            await _context.Races.AddAsync(newRace);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Race>> ReadAllRaces()
        {
            var races = await _context.Races.ToListAsync();
            return races;
        }

        public async Task<ICollection<Race>> ReadRaceAsync(int id)
        {
            var user = await _context.Users.FirstAsync(x => x.Id == id);
            var userRaces = await  _context.Races.Where(x => x.RacerOne == user.Username || x.RacerTwo == user.Username).ToListAsync();
            return userRaces;
        }
    }
}
