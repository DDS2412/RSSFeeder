using RSSFeeder.Model.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RSSFeeder.ViewModel
{
    public class OptionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Settings Manager
        /// </summary>
        private OptionManager optionManager;

        /// <summary>
        /// To go to another page
        /// </summary>
        public Action<int> ChangeModule { private get; set; }

        // Properties
        public ObservableCollection<NewsFeedOption> NewsFeedOptions
        {
            get
            {
                return new ObservableCollection<NewsFeedOption>(optionManager.Option.NewsFeedOptions);
            }
            set
            {
                optionManager.Option.NewsFeedOptions = new List<NewsFeedOption>(value);
                OnPropertyChanged("NewsFeedOptions");
            }
        }

        public string UpdatedButtonStatus
        {
            get { return optionManager.UpdatedButtonStatus; }
            set
            {
                optionManager.UpdatedButtonStatus = value;
                OnPropertyChanged("UpdatedButtonStatus");
            }
        }

        public string AddedButtonStatus
        {
            get { return optionManager.AddedButtonStatus; }
            set
            {
                optionManager.AddedButtonStatus = value;
                OnPropertyChanged("AddedButtonStatus");
            }
        }

        public OptionViewModel()
        {
            optionManager = new OptionManager();
        }

        /// <summary>
        /// Removes the RSS feed by id
        /// </summary>
        /// <param name="uid">index of RSS feed</param>
        public void DelRSSByIndex(string uid)
        {
            optionManager.DelNewsRSS(int.Parse(uid));
            optionManager.UpdateOption();
            OnPropertyChanged("NewsFeedOptions");
            OnPropertyChanged("UpdatedButtonStatus");
            OnPropertyChanged("AddedButtonStatus");
        }

        /// <summary>
        /// Adds new RSS feed
        /// </summary>
        public void AddNewsRSS()
        {
            optionManager.AddNewsRSS();
            optionManager.UpdateOption();
            OnPropertyChanged("NewsFeedOptions");
            OnPropertyChanged("UpdatedButtonStatus");
            OnPropertyChanged("AddedButtonStatus");
        }

        /// <summary>
        /// Go to the main page
        /// </summary>
        public void GoToRSSFeed() => ChangeModule(1);

        /// <summary>
        /// Updates option status 
        /// </summary>
        public void UpdateOption()
        {
            optionManager.UpdateOption();
            OnPropertyChanged("UpdatedButtonStatus");
        }

        /// <summary>
        /// Confirm changes to the RSS feed settings
        /// </summary>
        public void Confirm()
        {
            optionManager.ConfirmСhanges();
            optionManager.UpdateOption();
            OnPropertyChanged("UpdatedButtonStatus");
        }

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}