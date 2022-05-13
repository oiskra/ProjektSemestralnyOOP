﻿using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task BuyCarAsync(int id, int userId)
        {
            bool ifExists = await _context.Market.AnyAsync(x => x.Id == id);
            if(ifExists)
            {
                Car car = await _context.Market.SingleAsync(x => x.Id == id);
                if (car.IsAvailable == false) return;
                car.IsAvailable = false;
                car.UserId = userId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateCarAsync(Car entity)
        {
            await _context.Market.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Car>> ReadCarsAsync(int userId)
        {
            var userCars = await _context.Market.Where(x => x.UserId == userId).ToListAsync();
            return userCars;
        }

        public List<Car> ReadMarketAsync()
        {
            var market = _context.Market.ToList();
            return market;
        }

        public async Task SellCarAsync(int id, int userId)
        {
            var ifExists = await _context.Market.AnyAsync(x => x.Id == id);
            if (ifExists)
            {
                var car = await _context.Market.SingleAsync(x => x.Id == id);
                if (car.IsAvailable && car.UserId == userId) return;
                car.IsAvailable = true;
                car.UserId = null;
                await _context.SaveChangesAsync();
            }
        }
    }
}
