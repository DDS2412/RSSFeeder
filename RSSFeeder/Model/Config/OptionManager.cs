using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RSSFeeder.Utils;

namespace RSSFeeder.Model.Config
{
    public class OptionManager : INotifyPropertyChanged
    {
        // Constants responsible for displaying buttons
        private const string VISAIBLE = "Visible";
        private const string HIDDEN = "Hidden";

        private Option option;
        private Option newOption;
        // Var responsible for displaying buttons
        private string updatedButtonStatus;
        private string addedButtonStatus;

        public string UpdatedButtonStatus
        {
            get { return updatedButtonStatus; }
            set
            {
                updatedButtonStatus = value;
                OnPropertyChanged("UpdatedButtonStatus");
            }
        }

        public string AddedButtonStatus
        {
            get { return addedButtonStatus; }
            set
            {
                addedButtonStatus = value;
                OnPropertyChanged("AddedButtonStatus");
            }
        }

        public Option Option
        {
            get { return newOption; }
            set
            {
                newOption = value;
                OnPropertyChanged("Option");
            }
        }

        public OptionManager()
        {
            updatedButtonStatus = HIDDEN;
            newOption = SetOption();
            option = (Option)newOption.Clone();
            addedButtonStatus = newOption.NewsFeedOptions.Count < 4 ? VISAIBLE : HIDDEN;
        }

        /// <summary>
        /// Update button status.
        /// Makes the button visible, in case of updating the settings
        /// </summary>
        public void UpdateOption()
        {
            if (!newOption.Equals(option))
            {
                UpdatedButtonStatus = VISAIBLE;
            }
            else
            {
                UpdatedButtonStatus = HIDDEN;
            }
        }

        /// <summary>
        /// Removes a news feed by id
        /// </summary>
        /// <param name="index">Index of news feed</param>
        public void DelNewsRSS(int index)
        {
            newOption.NewsFeedOptions.RemoveAt(index);
            for(int i = 0; i < newOption.NewsFeedOptions.Count; i++)
            {
                newOption.NewsFeedOptions[i].NumberOfRSSFeed = i;
            }
            AddedButtonStatus = newOption.NewsFeedOptions.Count < 4 ? VISAIBLE : HIDDEN;
        }

        /// <summary>
        /// Adds new news feed
        /// </summary>
        public void AddNewsRSS()
        {
            if (newOption.NewsFeedOptions.Count < 4)
            {
                newOption.NewsFeedOptions.Add(NewsFeedOption.DefaultOption());
                newOption.NewsFeedOptions[newOption.NewsFeedOptions.Count - 1].NumberOfRSSFeed =
                    newOption.NewsFeedOptions.Count - 1;
            }
            AddedButtonStatus = newOption.NewsFeedOptions.Count < 4 ? VISAIBLE : HIDDEN;
        }

        /// <summary>
        /// Accept new settings
        /// </summary>
        public void ConfirmСhanges()
        {
            var optionIO = new OptionIO();
            optionIO.UpdateOption(newOption);
            option = (Option)newOption.Clone();
        }

        /// <summary>
        /// Getting settings from the settings file
        /// </summary>
        /// <returns></returns>
        private Option SetOption()
        {
            var optionIO = new OptionIO();

            return optionIO.GetOption();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
