using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{

    ///<summary>Defines methods that operate with database and User entity</summary>
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<bool> LoginUserAsync(string login, string password);
        Task<User> ReadUserAsync(int id);
        Task<ICollection<User>> ReadAllAsync();
        Task UpdateUserAsync(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
