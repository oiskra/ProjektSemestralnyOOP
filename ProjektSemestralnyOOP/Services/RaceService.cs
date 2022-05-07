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

        public async Task CreateRaceAsync(Race entity)
        {
            await _context.Races.AddAsync(entity);
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
