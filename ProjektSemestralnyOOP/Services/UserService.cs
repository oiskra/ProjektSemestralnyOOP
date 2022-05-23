using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) { return false; }

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

        public async Task<List<User>> ReadAllAsync()
        {
            List<User> allUsers = await _context.Users.ToListAsync();
            return allUsers;
        }

        public async Task<User> ReadUserAsync(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task RegisterUserAsync(User user)
        {
            bool ifExists = await _context.Users.AnyAsync(x => x.Id == user.Id);
            if (!ifExists)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return;
            }
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.Id);

            user.Username = updatedUser.Username;
            user.Login = updatedUser.Login;
            user.Password = updatedUser.Password;
            
            await _context.SaveChangesAsync();
        }
    }
}
