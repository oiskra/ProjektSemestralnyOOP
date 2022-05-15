﻿using ProjektSemestralnyOOP.MVVM.Model;
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
        public void LoginUser(User user)
        {
            UserLogged?.Invoke(user);
        }

        public void UpdateYourCars(List<Car> list)
        {
            YourCarsUpdated?.Invoke(list);
        }
    }
}
