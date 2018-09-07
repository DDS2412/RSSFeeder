using RSSFeeder.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace RSSFeeder.View
{
    public partial class OptionUserControl : UserControl
    {
        public OptionUserControl()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e) => ((OptionViewModel)DataContext).GoToRSSFeed();

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => ((OptionViewModel)DataContext).UpdateOption();

        private void Confirm_Click(object sender, RoutedEventArgs e) => ((OptionViewModel)DataContext).Confirm();
       
        private void Title_TextChanged(object sender, TextChangedEventArgs e) => ((OptionViewModel)DataContext).UpdateOption();

        private void FormattedTags_Checked(object sender, RoutedEventArgs e) => ((OptionViewModel)DataContext).UpdateOption();

        private void FormattedTags_Unchecked(object sender, RoutedEventArgs e) => ((OptionViewModel)DataContext).UpdateOption();

        private void Add_Click(object sender, RoutedEventArgs e) => ((OptionViewModel)DataContext).AddNewsRSS();

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            ((OptionViewModel)DataContext).DelRSSByIndex(
                ((Button)sender).Uid);
        } 
    }
}
