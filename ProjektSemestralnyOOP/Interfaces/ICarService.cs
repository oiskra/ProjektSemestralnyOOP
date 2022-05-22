using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    
    ///<summary>Defines methods that operate with database and Car entity</summary>
    public interface ICarService
    {
        Task CreateCarAsync(Car entity);
        List<Car> ReadCarsAsync(int userId);
        List<Car> ReadMarketAsync();
        Task BuyCarAsync(int id, int userId);
        Task SellCarAsync(int id, int userId);
        Task AddStatistic(Statistic entity);
    }
}
