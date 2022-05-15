﻿using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    ///<summary>Defines methods that operate with database and Race entity</summary>
    public interface IRaceService
    {
        Task CreateRaceAsync(string loggedUsername, string challengedUsername, Car loggedUserCar);
        Task<ICollection<Race>> ReadRaceAsync(int id);
        Task<ICollection<Race>> ReadAllRaces();
    }
}
