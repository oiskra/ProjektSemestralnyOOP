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
        private readonly RacingDBContext _context;

        public UserService(RacingDBContextFactory context)
        {
            _context = context.CreateDbContext();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> LoginUserAsync(string login, string password)
        {
            try
            {
                User loggedUser = await _context.Users.FirstAsync(x => x.Login == login && x.Password == password);
                return loggedUser;
            }
            catch { return null; }
        }

        public async Task<ICollection<User>> ReadAllAsync()
        {
            var allUsers = await _context.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User> ReadUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task RegisterUserAsync(User user)
        {
            var ifExists = await _context.Users.AnyAsync(x => x.Id == user.Id);
            if (!ifExists)
            {
                var addedUser = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return;
            }
        }

        public async Task UpdateUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
