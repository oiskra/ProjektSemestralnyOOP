using ProjektSemestralnyOOP.Commands;
using System;
using System.ComponentModel;
using ProjektSemestralnyOOP.Interfaces;
using ProjektSemestralnyOOP.Services;
using System.Windows;

namespace ProjektSemestralnyOOP.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public StartUpViewModel StartUpVM { get; }

        public MainViewModel()
        {
            StartUpVM = new StartUpViewModel();
            _currentView = StartUpVM;
        }
    }
}
