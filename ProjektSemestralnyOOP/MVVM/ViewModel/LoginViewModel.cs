using ProjektSemestralnyOOP.Commands;
using System;
using System.Windows.Input;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using ProjektSemestralnyOOP.DBcontext;
using System.Threading.Tasks;
using System.Windows;
using ProjektSemestralnyOOP.MVVM.Model;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ViewModelMediator _mediator;
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
        public LoginWindow Window { get; }

        public ICommand SubmitButton { get; }
        public ICommand CancelButton { get; }

        public LoginViewModel(LoginWindow window, ViewModelMediator mediator)
        {
            SubmitButton = new RelayCommand(SubmitLoginCommand);
            CancelButton = new RelayCommand(CancelLoginCommand);
            Window = window;
            _mediator = mediator;
        }

        private void CancelLoginCommand()
        {
            Window.Hide();
        }

        private async void SubmitLoginCommand()
        {
            IUserService service = new UserService(new RacingDBContextFactory());
            User loggedUser = await service.LoginUserAsync(Login, Password);

            if (loggedUser is User)
            {
                _mediator.LoginUser(loggedUser);
                MessageBox.Show("True", "islogged?");
                Window.Hide();
                Password = null;
                Login = null;
                return;
            }

            MessageBox.Show("False", "islogged?");
        }
    }
}
