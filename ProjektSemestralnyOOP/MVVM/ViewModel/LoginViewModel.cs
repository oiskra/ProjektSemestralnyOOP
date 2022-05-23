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
        private LoginWindow _window; 
        
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

        public ICommand SubmitButton { get; }
        public ICommand CancelButton { get; }

        public LoginViewModel(LoginWindow window, ViewModelMediator mediator)
        {
            SubmitButton = new RelayCommand(SubmitLoginCommand, () => 
                !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password));
            CancelButton = new RelayCommand(CancelLoginCommand);
            
            _window = window;
            _mediator = mediator;
        }

        private void CancelLoginCommand()
        {
            _window.Hide();
            Password = null;
            Login = null;
        }

        private async void SubmitLoginCommand()
        {
            IUserService service = new UserService(new RacingDBContextFactory());
            User loggedUser = await service.LoginUserAsync(Login, Password);

            if (loggedUser is User)
            {
                _mediator.LoginUser(loggedUser);
                MessageBox.Show("True", "islogged?");
                _window.Hide();
                Password = null;
                Login = null;
                return;
            }

            MessageBox.Show("False", "islogged?");
        }
    }
}
