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
        public event Action ProfileInfoUpdated;
        public event Action<List<Tuple<Car, Statistic>>> YourCarsUpdated;
        public event Action<List<Tuple<Car, Statistic>>> MarketUpdated;
        public event Action UserLoggedOut;
        
        public void LoginUser(User user)
        {
            UserLogged?.Invoke(user);
        }

        public void UpdateProfileInfo()
        {
            ProfileInfoUpdated?.Invoke();
        }

        public void UpdateYourCars(List<Tuple<Car, Statistic>> list)
        {
            YourCarsUpdated?.Invoke(list);
        }

        public void UpdateMarket(List<Tuple<Car, Statistic>> list)
        {
            MarketUpdated?.Invoke(list);
        }

        public void LogOutUser()
        {
            UserLoggedOut?.Invoke();
        }
    }
}
