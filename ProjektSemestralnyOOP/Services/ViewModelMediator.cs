using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.Services
{
    public class ViewModelMediator
    {
        public event Action<User> UserLogged;
        public event Action<List<Car>> YourCarsUpdated;
        public event Action<List<Car>> MarketUpdated;
        public event Action UserLoggedOut;
        
        public void LoginUser(User user)
        {
            UserLogged?.Invoke(user);
        }

        public void UpdateYourCars(List<Car> list)
        {
            YourCarsUpdated?.Invoke(list);
        }

        public void UpdateMarket(List<Car> list)
        {
            MarketUpdated?.Invoke(list);
        }

        public void LogOutUser()
        {
            UserLoggedOut?.Invoke();
        }
    }
}
