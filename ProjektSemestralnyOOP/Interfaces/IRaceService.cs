using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    ///<summary>Defines methods that operate with database and Race entity</summary>
    public interface IRaceService
    {
        Task CreateRaceAsync(string loggedUsername, string challengedUsername, Tuple<Car, Statistic> loggedUserCar);
        Task<List<Race>> ReadRaceAsync(int id);
        Task<List<Race>> ReadAllRacesAsync();
    }
}
