using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Provides methods that operate with database and <see cref="Car"/> entity 
    /// </summary>
    public class CarService : ICarService
    {
        private readonly RacingDBContext _context;

        /// <summary>
        /// Initializes new instance of <see cref="CarService"/> class.
        /// </summary>
        /// <param name="contextFactory"></param>
        public CarService(RacingDBContextFactory contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        /// <summary>
        /// Method that asynchronously selects row by id ( param <b>carId</b> ) from DB.
        /// Sets <see cref="Car.IsAvailable"/> to false and <see cref="Car.UserId"/> to userID passed in parameter
        /// </summary>
        /// <param name="carId">Id of a <see cref="Car"/> that needs to be updated</param>
        /// <param name="userId">Id of a <see cref="User"/> that will be assigned to selected row</param>
        /// <returns><see cref="Task"/></returns>
        public async Task BuyCarAsync(int carId, int userId)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            Car car = await _context.Market.SingleAsync(x => x.Id == carId);
            if (car.IsAvailable == false) return;
            if(car.Price > user.Money)
            {
                MessageBox.Show("You can\'t afford the car", "Info");
                return;
            }
            car.IsAvailable = false;
            car.UserId = userId;
            user.Money -= car.Price;
            await _context.SaveChangesAsync();
            MessageBox.Show($"You bought {car.Brand} {car.Model}.", "Info");
        }

        /// <summary>
        /// Method that asynchronously adds a new entry to database Market Table.
        /// </summary>
        /// <param name="entity"><see cref="Car"/> object inserted to DB.</param>
        /// <returns><see cref="Task"/></returns>
        public async Task CreateCarAsync(Car carEntity, Statistic statisticEntity)
        {

            Match brandValidator = Regex.Match(carEntity.Brand, @"^(\D{3,20})$|^(\D{3,20}\s{1}\D{3,20})$");
            Match modelValidator = Regex.Match(carEntity.Model, @"^(\S{1,20})$|^(\w{1,10}\s{1}\w{1,10})$");

            if (brandValidator.Success && modelValidator.Success)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var ok = await _context.Market.AddAsync(carEntity);
                        await _context.SaveChangesAsync();

                        statisticEntity.CarId = ok.Entity.Id;
                        await AddStatistic(statisticEntity);
                        await transaction.CommitAsync();
                        MessageBox.Show("Car created successfully", "Info");
                        return;

                    }
                    catch (ArgumentException e)
                    {
                        await transaction.RollbackAsync();
                        MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                
            }
            MessageBox.Show("Wrong values for brand or model.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;

        }

        /// <summary>
        /// Method that returns list of cars with assigned userId passed in parameter.
        /// </summary>
        /// <param name="userId">Id of <see cref="User"> that assigned cars need to be returned</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a
        /// <see cref="List{T}"/> that contains elements from the input sequence.</returns>
        public async Task<List<Tuple<Car,Statistic>>> ReadCarsAsync(int userId)
        {
            var userCars = from m in _context.Market
                         join s in _context.Statistics
                         on m.Id equals s.CarId
                         where m.UserId == userId
                         select new Tuple<Car, Statistic>(m, s);

            return await userCars.ToListAsync();
        }

        /// <summary>
        /// Method that returns entire Market Table from database as a list.
        /// </summary>
        /// <returns>Task with result of List of <see cref="Tuple{Car, Statistic}"/> objects.</returns>
        public async Task<List<Tuple<Car, Statistic>>> ReadMarketAsync()
        {
            var market = from m in _context.Market
                         join s in _context.Statistics
                         on m.Id equals s.CarId
                         select new Tuple<Car, Statistic>(m, s);

            return await market.ToListAsync();
        }

        /// <summary>
        /// Method that asynchronously selects row by id ( param <b>carId</b> ) from DB.
        /// Sets <see cref="Car.IsAvailable"/> to true and <see cref="Car.UserId"/> to null.
        /// </summary>
        /// <param name="carId">Id of a <see cref="Car"/> that needs to be updated</param>
        /// <param name="userId">Id of a <see cref="User"/> assigned to selected row</param>
        /// <returns><see cref="Task"/></returns>
        public async Task SellCarAsync(int carId, int userId)
        {
            bool ifExists = await _context.Market.AnyAsync(x => x.Id == carId);
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
                MessageBox.Show($"You sold {car.Brand} {car.Model}.", "Info");
            }
        }

        /// <summary>
        /// Method that asynchronously adds a new entry to database Statistic Table.
        /// </summary>
        /// <param name="entity"><see cref="Statistic"/> object inserted to DB.</param>
        /// <returns><see cref="Task"/></returns>
        public async Task AddStatistic(Statistic entity)
        {
            Regex statisticValidator = new(@"(^[1-9]$)|(^[1][0]$)");
            bool statisticSuccess = statisticValidator.IsMatch(entity.Speed.ToString())
                && statisticValidator.IsMatch(entity.Acceleration.ToString())
                && statisticValidator.IsMatch(entity.Grip.ToString())
                && statisticValidator.IsMatch(entity.Braking.ToString());

            if(!statisticSuccess)
                throw new ArgumentException("Statistic values have to be in range of 1 to 10");

            await _context.Statistics.AddAsync(entity);
            await _context.SaveChangesAsync();

        }
    }
}
