using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    
    ///<summary>Defines methods that operate with database and Car entity</summary>
    public interface ICarService
    {
        Task CreateCarAsync(Car carEntity, Statistic statisticEntity);
        Task<List<Tuple<Car, Statistic>>> ReadCarsAsync(int userId);
        Task<List<Tuple<Car, Statistic>>> ReadMarketAsync();
        Task BuyCarAsync(int carId, int userId);
        Task SellCarAsync(int carId, int userId);
        Task AddStatistic(Statistic entity);
    }
}
