using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly ViewModelMediator _mediator; 
        private readonly User _loggedUser;

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private int _money;
        public int Money
        {
            get => _money;
            set
            {
                _money = value;
                OnPropertyChanged(nameof(Money));
            }
        }


        public ProfileViewModel(User user, ViewModelMediator mediator)
        {
            _mediator = mediator;
            _mediator.ProfileInfoUpdated += OnProfileInfoUpdated;
            _loggedUser = user;
        }

        private async void OnProfileInfoUpdated()
        {
            IUserService userService = new UserService(new());
            User user = await userService.ReadUserAsync(_loggedUser.Id);
            _id = user.Id;
            Username = user.Username ?? "alsikfj";
            Login = user.Login;
            Password = user.Password;
            Money = user.Money;
        }
    }
}
