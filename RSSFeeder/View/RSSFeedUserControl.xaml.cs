using RSSFeeder.Utils;
using RSSFeeder.ViewModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RSSFeeder.View
{
    public partial class RSSFeedUserControl : UserControl
    {
        public RSSFeedUserControl()
        {
            InitializeComponent();
        }

        private void Сhoice_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            TabMenu.SelectedIndex = index;
        }

        private void TabMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ((ListBox)e.Source).SelectedIndex;

            GridMain.Background = Colors.GetColorById(index);
            GridCursor.Margin = new Thickness(10 + (168 * index), 65, 0, 0);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ((RSSFeedViewModel)DataContext).Exit();
        }

        private void GoToOption_Click(object sender, RoutedEventArgs e)
        {
            ((RSSFeedViewModel)DataContext).GoToOption();
        }

        private void Lent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var articleIndex = ((ListBox)e.Source).SelectedIndex;
            ((RSSFeedViewModel)DataContext).GoToSelectedArticle(articleIndex);
        }
    }
}
