using System.Windows.Media;

namespace RSSFeeder.Utils
{
    public static class Colors
    {
        /// <summary>
        /// List of colors
        /// </summary>
        private static SolidColorBrush[] colors = 
            { Brushes.Aquamarine,Brushes.Beige, Brushes.HotPink, Brushes.CadetBlue};

        /// <summary>
        /// Returns the color depending on the page's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SolidColorBrush GetColorById(int id) => colors[id % 4];
    }
}
