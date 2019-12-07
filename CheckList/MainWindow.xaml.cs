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
using Microsoft.Win32;

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

        private void ResetUI ()
        {
            ChangeListName("New List");
            StackPanel listPanel = (StackPanel)this.FindName("ListPanel");
            listPanel.Children.Clear();
        }

        private void LoadUI ()
        {
            ResetUI();

            List list = new FileSystem().Load();

            ChangeListName(list.name);

            for (int i = 0; i < list.elements.Count; i++)
            {
                CreateNewListElement(list.elements[i].name, list.elements[i].done, false);
            }
        }

        private void SaveUI ()
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

        private void MainWindow_Closing(object sender, EventArgs e)
        {
            SaveUI();
        }

        private void Initialize ()
        {
            LoadUI();
        }

        private string GetListName ()
        {
            return (string)((CheckBox)this.FindName("ListName")).Content;
        }

        private void ChangeListName (string name)
        {
            ((CheckBox)this.FindName("ListName")).Content = name; 
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

        private void AddElementMenu_Click(object sender, RoutedEventArgs e)
        {
            CreateNewListElement("New List Element", false, true);
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.Title = "Open";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileSystem.SAVE_DIRECTORY = dialog.FileName;
                LoadUI();
            }
        }

        private void MenuItemSaveAs_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                FileSystem.SAVE_DIRECTORY = dialog.FileName;
                SaveUI();
            }
        }

        private void MenuItemNewList_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                FileSystem.SAVE_DIRECTORY = dialog.FileName;
                ResetUI();
                SaveUI();
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SaveUI();
        }

        private void MenuItemEditListName_Click (object sender, RoutedEventArgs e)
        {
            OpenEditWindow((CheckBox)this.FindName("ListName"));
        }
    }
}
