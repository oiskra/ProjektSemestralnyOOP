using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    public class RaceService : IRaceService
    {
        public Task CreateRaceAsync(Race entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Race>> ReadAllRaces()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Race>> ReadRaceAsync()
        {
            throw new NotImplementedException();
        }
    }
}
