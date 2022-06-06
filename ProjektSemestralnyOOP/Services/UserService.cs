using Microsoft.EntityFrameworkCore;
using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Initializes new instance of <see cref="UserService"/> class
        /// </summary>
        /// <param name="context"></param>
        public UserService(RacingDBContextFactory contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        /// <summary>
        /// Method that asynchronously deletes user from DB with provided id. 
        /// </summary>
        /// <param name="id">Id of <see cref="User"/> to delete.</param>
        /// <returns>Task operation with result true if user was successfully delete, false otherwise</returns>
        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }

        }

        /// <summary>
        /// Method that asynchronously searches for <see cref="User"/> in database by provided login and password.
        /// </summary>
        /// <param name="login"><see cref="User"/> property Login.</param>
        /// <param name="password"><see cref="User"/> property Password.</param>
        /// <returns>Task with result of <see cref="User"/> if such exists, and null otherwise.</returns>
        public async Task<User> LoginUserAsync(string login, string password)
        {
            try
            {
                User loggedUser = await _context.Users.FirstAsync(x => x.Login == login && x.Password == password);
                return loggedUser;
            }
            catch { return null; }
        }

        /// <summary>
        /// Method that asynchronously returns entire User Table from database. 
        /// </summary>
        /// <returns>Task with result of List of <see cref="User"/> objects.</returns>
        public async Task<List<User>> ReadAllAsync()
        {
            List<User> allUsers = await _context.Users.ToListAsync();
            return allUsers;
        }

        /// <summary>
        /// Method that asynchronously finds and returns <see cref="User"/> with provided id. 
        /// </summary>
        /// <param name="id">Id of <see cref="User"/> that needs to be found.</param>
        /// <returns>Task with result of <see cref="User"/> object.</returns>
        public async Task<User> ReadUserAsync(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        /// <summary>
        /// Method that asynchronously creates a new database entry based on provided <see cref="User"/> object.
        /// </summary>
        /// <param name="user"><see cref="User"/> that is inserted to database.</param>
        /// <returns><see cref="Task"/></returns>
        public async Task RegisterUserAsync(User user)
        {
            Match passwordValidator = Regex.Match(user.Password, @"^\S{8,}$");
            Regex usernameAndLoginValidator = new(@"^\S{4,}$");
            bool match = usernameAndLoginValidator.IsMatch(user.Login) && usernameAndLoginValidator.IsMatch(user.Username);
            
            if(!passwordValidator.Success)
            {
                MessageBox.Show("Password can\'t have whitespaces and must be at least 8 characters long", "Info");
                return;
            }

            if(!match)
            {
                MessageBox.Show("Username and login can\'t have whitespaces and must be at least 4 characters long", "Info");
                return;
            }

            bool ifExists = await _context.Users.AnyAsync(x => x.Login == user.Login || x.Username == user.Username);
            if (!ifExists)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                MessageBox.Show("User registerd successfully", "Info");
                return;
            }

            MessageBox.Show("This username or login is already taken. Try again.", "Info");
        }

        /// <summary>
        /// Method that asynchronously find a <see cref="User"/> in database and updates columns: Login, Password, Username.
        /// </summary>
        /// <param name="updatedUser"><see cref="User"/> object with values to update.</param>
        /// <returns><see cref="Task"/></returns>
        public async Task<bool> UpdateUserAsync(User updatedUser)
        {
            Match passwordValidator = Regex.Match(updatedUser.Password, @"^\S{8,}$");
            Regex usernameAndLoginValidator = new(@"^\S{4,}$");
            bool match = usernameAndLoginValidator.IsMatch(updatedUser.Login) && usernameAndLoginValidator.IsMatch(updatedUser.Username);

            if (!passwordValidator.Success)
            {
                MessageBox.Show("Password can\'t have whitespaces and must be at least 8 characters long", "Info");
                return false;
            }

            if (!match)
            {
                MessageBox.Show("Username and login can\'t have whitespaces and must be at least 4 characters long", "Info");
                return false;
            }

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updatedUser.Id);

            user.Username = updatedUser.Username;
            user.Login = updatedUser.Login;
            user.Password = updatedUser.Password;
            
            await _context.SaveChangesAsync();
            MessageBox.Show("User account updated successfully.", "Info");
            return true;
        }
    }
}
