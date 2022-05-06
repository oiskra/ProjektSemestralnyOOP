using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    public interface ICarService
    {
        Task CreateCarAsync(Car entity);
        Task<ICollection<Car>> ReadCarsAsync();
        Task<ICollection<Car>> ReadMarketAsync();
        Task BuyCarAsync(int id);
        Task SellCarAsync(int id);
    }
}
