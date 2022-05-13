using ProjektSemestralnyOOP.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly User _loggedUser;

        public string Id => _loggedUser.Id.ToString();
        public string Username => _loggedUser.Username;
        public string Login => _loggedUser.Login;
        public string Password => _loggedUser.Password;
        public string Money => _loggedUser.Money.ToString();

        public ProfileViewModel(User user)
        {
            _loggedUser = user;
        }
    }
}
