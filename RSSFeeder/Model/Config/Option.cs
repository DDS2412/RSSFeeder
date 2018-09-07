using System;
using System.Collections.Generic;

namespace RSSFeeder.Model.Config
{
    public class Option : ICloneable
    {
        public List<NewsFeedOption> NewsFeedOptions { get; set; }

        public object Clone()
        {
            var newsFeedOptions = new List<NewsFeedOption>();
            foreach(var n in NewsFeedOptions)
            {
                newsFeedOptions.Add((NewsFeedOption)n.Clone());
            }

            return new Option
            {
                NewsFeedOptions = newsFeedOptions
            };
        }

        public override bool Equals(object obj)
        {
            var probOption = obj as Option;
            bool isEqual = !(probOption is null);
            if (isEqual)
            {
                isEqual = probOption.NewsFeedOptions.Count == this.NewsFeedOptions.Count;
                if (isEqual)
                {
                    for (int i = 0; i < this.NewsFeedOptions.Count; i++)
                    {
                        isEqual = probOption.NewsFeedOptions[i].MaxArticles == this.NewsFeedOptions[i].MaxArticles &&
                            probOption.NewsFeedOptions[i].NumberOfRSSFeed == this.NewsFeedOptions[i].NumberOfRSSFeed &&
                            probOption.NewsFeedOptions[i].Title.Equals(this.NewsFeedOptions[i].Title) &&
                            probOption.NewsFeedOptions[i].UpdateFrequency == this.NewsFeedOptions[i].UpdateFrequency &&
                            probOption.NewsFeedOptions[i].IsFormattedTags == this.NewsFeedOptions[i].IsFormattedTags &&
                            probOption.NewsFeedOptions[i].RSSUrl.Equals(this.NewsFeedOptions[i].RSSUrl);
                            if (!isEqual)
                                break;
                    }
                }
            }
            return isEqual;
        }
    }
}
