using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    
    ///<summary>Defines methods that operate with database and Car entity</summary>
    public interface ICarService
    {
        Task CreateCarAsync(Car entity);
        Task<ICollection<Car>> ReadCarsAsync();
        Task<ICollection<Car>> ReadMarketAsync();
        Task BuyCarAsync(int id);
        Task SellCarAsync(int id);
    }
}
