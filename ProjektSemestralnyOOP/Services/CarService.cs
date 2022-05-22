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
    /// Provides methods that operate with database and Car entity 
    /// </summary>
    public class CarService : ICarService
    {
        private readonly RacingDBContext _context;

        public CarService(RacingDBContextFactory context)
        {
            _context = context.CreateDbContext();
        }

        public async Task BuyCarAsync(int carId, int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            Car car = await _context.Market.SingleAsync(x => x.Id == carId);
            if (car.IsAvailable == false) return;
            if(car.Price > user.Money)
            {
                MessageBox.Show("You can\'t afford the car");
                return;
            }
            car.IsAvailable = false;
            car.UserId = userId;
            user.Money -= car.Price;
            await _context.SaveChangesAsync();
        }

        public async Task CreateCarAsync(Car entity)
        {
            await _context.Market.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Car>> ReadCarsAsync(int userId)
        {
            List<Car> userCars = await _context.Market.Where(x => x.UserId == userId).ToListAsync();
            return userCars;
        }

        public async Task<List<Tuple<Car, Statistic>>> ReadMarketAsync()
        {
            var market = from m in _context.Market
                         join s in _context.Statistics
                         on m.Id equals s.CarId
                         select new Tuple<Car, Statistic>(m, s);

            return await market.ToListAsync();
        }

        public async Task SellCarAsync(int carId, int userId)
        {
            bool ifExists = await _context.Market.AnyAsync(x => x.Id == id);
            if (ifExists)
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                Car car = await _context.Market.SingleAsync(x => x.Id == carId);
                if (car.IsAvailable && car.UserId == userId)
                    return;
                car.IsAvailable = true;
                car.UserId = null;
                user.Money += car.Price;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddStatistic(Statistic entity)
        {
            try
            {
                await _context.Statistics.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());   
            }
        }
    }
}
