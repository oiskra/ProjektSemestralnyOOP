using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Provides methods that operate with database and Race entity 
    /// </summary>
    public class RaceService : IRaceService
    {
        private readonly RacingDBContext _context;

        public RaceService(RacingDBContext context)
        {
            _context = context;
        }

        public async Task CreateRaceAsync(Race entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Race>> ReadAllRaces()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Race>> ReadRaceAsync()
        {
            throw new NotImplementedException();
        }
    }
}
