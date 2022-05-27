using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    /// <summary>
    /// Class responsible for viewmodels communication.
    /// </summary>
    public class ViewModelMediator
    {
        /// <summary>
        /// Event that is invoked when user logged in and sends user object to subscribed classes. 
        /// </summary>
        public event Action<User> UserLogged;

        /// <summary>
        /// Event that is invoked when Profile tab is opened. 
        /// </summary>
        public event Action ProfileInfoUpdated;

        /// <summary>
        /// Event that is invoked when YourCars tab is opened and sends list of cars to subscribed classes.
        /// </summary>
        public event Action<List<Tuple<Car, Statistic>>> YourCarsUpdated;

        /// <summary>
        /// Event that is invoked when Market tab is opened and sends list of cars to subscribed classes. 
        /// </summary>
        public event Action<List<Tuple<Car, Statistic>>> MarketUpdated;

        /// <summary>
        /// Event that is invoked when user logged out.
        /// </summary>
        public event Action UserLoggedOut;

        /// <summary>
        /// Method that invokes <see cref="UserLogged"/> event.
        /// </summary>
        /// <param name="user">Object of logged in user</param>
        public void LoginUser(User user)
        {
            UserLogged?.Invoke(user);
        }

        /// <summary>
        /// Method that invokes <see cref="ProfileInfoUpdated"/> event.
        /// </summary>
        public void UpdateProfileInfo()
        {
            ProfileInfoUpdated?.Invoke();
        }

        /// <summary>
        /// Method that invokes <see cref="YourCarsUpdated"/> event. 
        /// </summary>
        /// <param name="list">List of logged in user's cars from database</param>
        public void UpdateYourCars(List<Tuple<Car, Statistic>> list)
        {
            YourCarsUpdated?.Invoke(list);
        }

        /// <summary>
        /// Method that invokes <see cref="MarketUpdated"/> event.
        /// </summary>
        /// <param name="list">List of cars from database</param>
        public void UpdateMarket(List<Tuple<Car, Statistic>> list)
        {
            MarketUpdated?.Invoke(list);
        }

        /// <summary>
        /// Method that invokes <see cref="UserLoggedOut"/> event.
        /// </summary>
        public void LogOutUser()
        {
            UserLoggedOut?.Invoke();
        }
    }
}
