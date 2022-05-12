using ProjektSemestralnyOOP.DBcontext;
using ProjektSemestralnyOOP.MVVM.Model;
using ProjektSemestralnyOOP.MVVM.ViewModel;
using ProjektSemestralnyOOP.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektSemestralnyOOP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void OnStartup(object sender, StartupEventArgs e)
        {
            //new mediatior
            ViewModelMediator mediator = new();
            MainWindow main = new MainWindow()
            {
                DataContext = new MainViewModel(mediator)
            };
            main.Show();
        }
    }
}
