using RSSFeeder.Model.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;
using System.Xml;

namespace RSSFeeder.Model
{
    public class NewsFeedManager
    {
        private Option option;
        private DispatcherTimer[] timers;

        public ObservableCollection<NewsFeed> NewsFeeds { get; set; }
        public NewsFeed FirsNewsFeed { get; }

        public NewsFeedManager(Option _option)
        {
            option = _option;
            NewsFeeds = CreateNewsFeed();
            if (NewsFeeds.Count > 0)
                FirsNewsFeed = NewsFeeds[0];
            InitTimers();
        }

        /// <summary>
        /// Initialize the timers that are responsible for the refresh rate
        /// </summary>
        private void InitTimers()
        {
            timers = new DispatcherTimer[NewsFeeds.Count];
            for(int i= 0; i < NewsFeeds.Count; i++)
            {
                timers[i] = new DispatcherTimer();
                timers[i].Interval = new TimeSpan(option.NewsFeedOptions[i].UpdateFrequency * TimeSpan.TicksPerSecond);
                timers[i].Tag = i;
                timers[i].Tick += new EventHandler(UpdateAsync);
                timers[i].Start();
            }
        }

        /// <summary>
        /// A handler that fires at a certain frequency
        /// Updates the list of articles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UpdateAsync(object sender, EventArgs e)
        {
            var timer = (DispatcherTimer)sender;
            var index = (int)timer.Tag;
            await Dispatcher.CurrentDispatcher.BeginInvoke(
                new Action(() => 
                {
                    NewsFeeds[index].News = GetNews(
                    option.NewsFeedOptions[index].MaxArticles,
                    option.NewsFeedOptions[index].RSSUrl,
                    option.NewsFeedOptions[index].IsFormattedTags);
                    NewsFeeds[index].UpdateNews();
                }));
        }

        /// <summary>
        /// Creates news feeds
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<NewsFeed> CreateNewsFeed()
        {
            var newsFeeds = new ObservableCollection<NewsFeed>();
            foreach (var newsFeedOption in option.NewsFeedOptions)
            {
                ObservableCollection<News> news = GetNews(newsFeedOption.MaxArticles, newsFeedOption.RSSUrl, newsFeedOption.IsFormattedTags);
                newsFeeds.Add(new NewsFeed
                {
                    Title = newsFeedOption.Title,
                    Number = newsFeedOption.NumberOfRSSFeed,
                    News = news
                });
            }
            return newsFeeds;
        }

        /// <summary>
        /// Returns a list of articles by the specified link
        /// </summary>
        /// <param name="maxArticles">Number of articles displayed</param>
        /// <param name="RSSUrl">Link on which we receive news</param>
        /// <param name="isFormattedTags">Format tags or not</param>
        /// <returns></returns>
        private ObservableCollection<News> GetNews(int maxArticles, string RSSUrl, bool isFormattedTags)
        {
            var news = new ObservableCollection<News>();
            try
            {
                XmlReader fdReadxml = XmlReader.Create(RSSUrl);
                SyndicationFeed fdFeed = SyndicationFeed.Load(fdReadxml);
                var fdFeedArr = fdFeed.Items.ToArray();
                for (int i = 0; i < maxArticles; i++)
                {
                    var categoriesArr = fdFeedArr[i].Categories.ToArray();
                    var categoriesStringsArr = categoriesArr.Select(n => n.Name).ToArray();

                    news.Add(
                            new News(isFormattedTags)
                            {
                                Number = i,
                                Title = fdFeedArr[i].Title.Text,
                                Date = fdFeedArr[i].PublishDate.DateTime,
                                Url = fdFeedArr[i].Id,
                                Categories = categoriesStringsArr,
                                
                            });
                }
            }
            catch { }

            return news;
        }
    }
}
