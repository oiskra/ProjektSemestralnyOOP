using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    public class UserService : IUserService
    {
        private readonly RacingDBContext _context;

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task LoginUserAsync(string login, string password)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> ReadUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task RegisterUserAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
