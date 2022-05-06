using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    public class CarService : ICarService
    {
        public Task BuyCarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateCarAsync(Car entity)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Car>> ReadCarsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Car>> ReadMarketAsync()
        {
            throw new NotImplementedException();
        }

        public Task SellCarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
