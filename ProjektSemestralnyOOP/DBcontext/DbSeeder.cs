using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.DBcontext
{
    /// <summary>
    /// Class responsible for seeding database.
    /// </summary>
    public class DbSeeder
    {
        /// <summary>
        /// Method that asynchronously seeds the database with specified entries.
        /// </summary>
        /// <param name="context">Database context to seed</param>
        public static async void Seed(RacingDBContext context)
        {
            if (context.Market.Any())
                return;

            var cars = new Car[]
            {
                new Car {Brand = "Dodge", Model = "Charger", IsAvailable = true, UserId = null, Price = 600},
                new Car {Brand = "Acura", Model = "NSX", IsAvailable = true, UserId = null, Price = 1000},
                new Car {Brand = "Mitsubishi", Model = "Lancer EVO X", IsAvailable = true, UserId = null, Price = 1000},
                new Car {Brand = "Toyota", Model = "GT86", IsAvailable = true, UserId = null, Price = 500},
                new Car {Brand = "Toyota", Model = "AE86 Trueno", IsAvailable = true, UserId = null, Price = 4000},
                new Car {Brand = "Mazda", Model = "RX-7", IsAvailable = true, UserId = null, Price = 1100},
                new Car {Brand = "Nissan", Model = "GTR R34", IsAvailable = true, UserId = null, Price = 2300},
                new Car {Brand = "Toyota", Model = "Supra", IsAvailable = true, UserId = null, Price = 2500},
                new Car {Brand = "Lexus", Model = "IS200", IsAvailable = true, UserId = null, Price = 1600},
                new Car {Brand = "Nissan", Model = "370Z", IsAvailable = true, UserId = null, Price = 2000}
            };

            await context.Market.AddRangeAsync(cars);
            await context.SaveChangesAsync();

            if (context.Statistics.Any())
                return;

            var statistics = new Statistic[]
            {
                new Statistic {Speed = 2, Acceleration= 2, Braking = 1, Grip = 1, CarId = 1},
                new Statistic {Speed = 5, Acceleration= 3, Braking = 1, Grip = 1, CarId = 2},
                new Statistic {Speed = 3, Acceleration= 3, Braking = 2, Grip = 2, CarId = 3},
                new Statistic {Speed = 2, Acceleration= 1, Braking = 1, Grip = 1, CarId = 4},
                new Statistic {Speed = 10, Acceleration= 10, Braking = 10, Grip = 10, CarId = 5},
                new Statistic {Speed = 4, Acceleration= 2, Braking = 2, Grip = 3, CarId = 6},
                new Statistic {Speed = 5, Acceleration = 7, Braking = 5, Grip = 6, CarId = 7},
                new Statistic {Speed = 7, Acceleration= 8, Braking = 5, Grip = 5, CarId = 8},
                new Statistic {Speed = 5, Acceleration= 3, Braking = 4, Grip = 4, CarId = 9},
                new Statistic {Speed = 5, Acceleration= 5, Braking = 5, Grip = 5, CarId = 10}
            };

            await context.Statistics.AddRangeAsync(statistics);
            await context.SaveChangesAsync();

            if (context.Users.Any())
                return;

            var adminAcc = new User
            {
                Username = "admin",
                Login = "admin",
                Password = "admin",
                Money = 0
            };

            await context.Users.AddAsync(adminAcc);
            await context.SaveChangesAsync();
        }
    }
}
