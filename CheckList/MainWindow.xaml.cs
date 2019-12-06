using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            EditNameWindow newWindow = new EditNameWindow();

            CheckBox checkBox = ((CheckBox)((Grid)((Menu)((MenuItem)((MenuItem)sender).Parent).Parent).Parent).Children[0]);
            newWindow.Initialize(checkBox);

            newWindow.Show();
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            StackPanel list = (StackPanel)this.FindName("ListPanel");
            list.Children.Remove(((Grid)((Menu)((MenuItem)((MenuItem)sender).Parent).Parent).Parent));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel list = (StackPanel)this.FindName("ListPanel");

            Grid newListElement = (Grid)(this.FindResource("ListElement"));

            list.Children.Add(newListElement);
        }
    }
}
