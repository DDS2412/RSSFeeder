using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace RSSFeeder.Model
{
    public class News : BaseNews
    {
        private DateTime date;
        private string url;
        private string[] categories;
        private bool isFormattedTags;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// Converting an array to a string with formatting
        /// </summary>
        public string CategoriesToString
        {
            get
            {
                var formattingLiteral = isFormattedTags ? "#" : " ";
                var stringBuilder = new StringBuilder();
                foreach(var str in categories)
                {
                    stringBuilder.Append(formattingLiteral);
                    stringBuilder.Append(
                            isFormattedTags ? str.ToUpper() : str);
                }
                
                return stringBuilder.ToString();
            }
        }

        public string[] Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged("Categories");
            }
        }
        
        public News(bool _isFormattedTags = false)
        {
            isFormattedTags = _isFormattedTags;
        }
    }
}
