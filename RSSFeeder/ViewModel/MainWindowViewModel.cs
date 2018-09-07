using RSSFeeder.Separate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace RSSFeeder.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Selected and loaded View and ViewModel pair
        /// </summary>
        private IModule _SelectedModule;

        /// <summary>
        /// List of all possible loaders for the View and ViewModel pairs
        /// </summary>
        public List<IModule> Modules { get; private set; }

        public IModule SelectedModule
        {
            get { return _SelectedModule; }
            set
            {

                if (value == _SelectedModule) return;
                if (_SelectedModule != null) _SelectedModule.Deactivate();
                _SelectedModule = value;

                OnPropertyChanged(nameof(SelectedModule));
                OnPropertyChanged("UserInterface");
            }
        }

        /// <summary>
        /// This View displays through ContentPresenter
        /// </summary>
        public UserControl UserInterface
        {
            get
            {
                if (SelectedModule == null) return null;
                SelectedModule.ChangeModule = ChangeModule;
                return SelectedModule.UserInterface;
            }
        }

        public MainWindowViewModel(IEnumerable<IModule> modules)
        {
            Modules = modules.ToList();
            if (Modules.Count > 0)  
                SelectedModule = Modules.First(n=> n is SeparateRSSFeed);   
        }

        /// <summary>
        /// Transition between models by id
        /// </summary>
        /// <param name="id"></param>
        public void ChangeModule(int id)
        {
            SelectedModule = Modules[id];
        }

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}