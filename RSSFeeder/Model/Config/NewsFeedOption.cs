using System;
using System.Windows.Data;

namespace RSSFeeder.Model.Config
{
    public class NewsFeedOption : ICloneable
    {
        public int NumberOfRSSFeed { get; set; }
        public string Title { get; set; }
        public int MaxArticles { get; set; }
        public int UpdateFrequency { get; set; }
        public string RSSUrl{ get; set; }
        public bool IsFormattedTags { get; set; }

        /// <summary>
        /// Returns the value for the default news feed settings
        /// </summary>
        /// <returns></returns>
        public static NewsFeedOption DefaultOption() => new NewsFeedOption
                {
                    Title = "Habr",
                    RSSUrl = @"https://habr.com/rss/interesting/",
                    IsFormattedTags = false,
                    MaxArticles = 6,
                    UpdateFrequency = 12
                };

        public object Clone()
        {
            return new NewsFeedOption
            {
                NumberOfRSSFeed = this.NumberOfRSSFeed,
                Title = this.Title,
                MaxArticles = this.MaxArticles,
                UpdateFrequency = this.UpdateFrequency,
                RSSUrl = this.RSSUrl,
                IsFormattedTags = this.IsFormattedTags
            };
        }
    }
}
