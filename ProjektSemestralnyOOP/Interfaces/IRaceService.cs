using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    ///<summary>Defines methods that operate with database and Race entity</summary>
    public interface IRaceService
    {
        Task CreateRaceAsync(Race entity);
        Task<ICollection<Race>> ReadRaceAsync();
        Task<ICollection<Race>> ReadAllRaces();
    }
}
