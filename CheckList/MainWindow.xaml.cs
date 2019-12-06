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
            Initialize();
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            List list = new List(GetListName());

            StackPanel listPanel = (StackPanel)this.FindName("ListPanel");

            for (int i = 0; i < listPanel.Children.Count; i++)
            {
                CheckBox listElement = (CheckBox)((Grid)listPanel.Children[i]).Children[0];
                list.elements.Add(new ListElement((string)listElement.Content));
                list.elements[list.elements.Count - 1].done = (bool)listElement.IsChecked;
            }

            list.UpdateDone();

            new FileSystem().Save(list);
        }

        private void Initialize ()
        {
            List list = new FileSystem().Load();

            ChangeListName(list.name);

            for (int i = 0; i < list.elements.Count; i++)
            {
                CreateNewListElement(list.elements[i].name, list.elements[i].done, false);
            }
        }

        private string GetListName ()
        {
            return ((TextBlock)this.FindName("ListName")).Text;
        }

        private void ChangeListName (string name)
        {
            ((TextBlock)this.FindName("ListName")).Text = name; 
        }

        private void OpenEditWindow(CheckBox checkBox)
        {
            EditNameWindow newWindow = new EditNameWindow();
            newWindow.Initialize(checkBox);
            newWindow.Owner = this;
            newWindow.Show();
        }

        private void CreateNewListElement (string defaultName, bool startDone, bool openEditorOnCreate)
        {
            StackPanel list = (StackPanel)this.FindName("ListPanel");
            Grid newListElement = (Grid)(this.FindResource("ListElement"));
            CheckBox checkBox = ((CheckBox)newListElement.Children[0]);
            checkBox.Content = defaultName;
            checkBox.IsChecked = startDone;

            list.Children.Add(newListElement);
            if (openEditorOnCreate)
            {
                OpenEditWindow(checkBox);
            }
        }

        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = ((CheckBox)((Grid)((Menu)((MenuItem)((MenuItem)sender).Parent).Parent).Parent).Children[0]);
            OpenEditWindow(checkBox);
        }

        private void DeleteElement_Click(object sender, RoutedEventArgs e)
        {
            StackPanel list = (StackPanel)this.FindName("ListPanel");
            list.Children.Remove(((Grid)((Menu)((MenuItem)((MenuItem)sender).Parent).Parent).Parent));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateNewListElement("New List Element", false, true);
        }
    }
}
