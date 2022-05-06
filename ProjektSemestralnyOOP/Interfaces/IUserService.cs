using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(User entity);
        Task LoginUserAsync(string login, string password);
        Task<User> ReadUserAsync();
        Task<ICollection<User>> ReadAllAsync();
        Task UpdateUserAsync(int id);
        Task<bool> DeleteUserAsync(int id);
    }
}
