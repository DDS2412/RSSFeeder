using RSSFeeder.Model.Config;
using System.Collections.ObjectModel;

namespace RSSFeeder.Model
{
    public class NewsFeed : BaseNews
    {
        public ObservableCollection<News> News { get; set; }  

        public void UpdateNews()
        {
            OnPropertyChanged("News");
        }
    }
}
