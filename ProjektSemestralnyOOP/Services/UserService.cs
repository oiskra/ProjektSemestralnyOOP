using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Provides methods that operate with database and User entity 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly RacingDBContextFactory _contextF;
        
        public UserService(RacingDBContextFactory context)
        {
            _contextF = context;
        }
        
        public async Task<bool> DeleteUserAsync(int id)
        {
            using RacingDBContext context = _contextF.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<User> LoginUserAsync(string login, string password)
        {
            using RacingDBContext context = _contextF.CreateDbContext();
            var loggedUser = await context.Users.FirstAsync(x => x.Login == login && x.Password == password);
            return loggedUser;
        }

        public async Task<ICollection<User>> ReadAllAsync()
        {
            using RacingDBContext context = _contextF.CreateDbContext();
            var allUsers = await context.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User> ReadUserAsync(int id)
        {
            using RacingDBContext context = _contextF.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task RegisterUserAsync(User user)
        {
            using RacingDBContext context = _contextF.CreateDbContext();
            var addedUser = await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id)
        {
            using RacingDBContext context = _contextF.CreateDbContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

    }
}
