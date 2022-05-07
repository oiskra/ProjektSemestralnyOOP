using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Provides methods that operate with database and Car entity 
    /// </summary>
    public class CarService : ICarService
    {
        private readonly RacingDBContext _context;

        public CarService(RacingDBContext context)
        {
            _context = context;
        }

        public async Task BuyCarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateCarAsync(Car entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Car>> ReadCarsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Car>> ReadMarketAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SellCarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
