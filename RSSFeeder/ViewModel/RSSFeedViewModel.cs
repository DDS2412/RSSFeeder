using RSSFeeder.Model;
using RSSFeeder.Model.Config;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Diagnostics;

namespace RSSFeeder.ViewModel
{
    public class RSSFeedViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Selected news feed
        /// </summary>
        private NewsFeed selectedNewsFeed;

        /// <summary>
        /// Settings Manager
        /// </summary>
        private OptionManager optionManager;
        /// <summary>
        /// News Manager
        /// </summary>
        private NewsFeedManager newsFeedManager;

        /// <summary>
        /// To go to another page
        /// </summary>
        public Action<int> ChangeModule { private get; set; }

        // Properties 
        public NewsFeed SelecetedNewsFeed
        {
            get { return selectedNewsFeed; }
            set
            {
                selectedNewsFeed = value;
                OnPropertyChanged("SelecetedNewsFeed");
            }
        }

        public ObservableCollection<NewsFeed> NewsFeeds
        {
            get { return newsFeedManager.NewsFeeds; }
            set
            {
                newsFeedManager.NewsFeeds = value;
                OnPropertyChanged("NewsFeeds");
            }
        }
        
        public RSSFeedViewModel()
        {
            optionManager = new OptionManager();
            newsFeedManager = new NewsFeedManager(optionManager.Option);
            selectedNewsFeed = newsFeedManager.FirsNewsFeed;
        }

        /// <summary>
        /// Go to the settings page
        /// </summary>
        public void GoToOption()
        {
            ChangeModule(0);
        }

        /// <summary>
        /// Shutdown the program
        /// </summary>
        public void Exit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Go to the browser using the article link
        /// </summary>
        /// <param name="articleIndex"></param>
        public void GoToSelectedArticle(int articleIndex) => Process.Start(selectedNewsFeed.News[articleIndex].Url);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}