using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{

    ///<summary>Defines methods that operate with database and User entity</summary>
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<User> LoginUserAsync(string login, string password);
        Task<User> ReadUserAsync(int id);
        Task<List<User>> ReadAllAsync();
        Task<bool> UpdateUserAsync(User updatedUser);
        Task<bool> DeleteUserAsync(int id);
    }
}
