using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CheckList
{
    /// <summary>
    /// Interaction logic for EditNameWindow.xaml
    /// </summary>
    public partial class EditNameWindow : Window
    {
        private CheckBox checkBoxInMain;

        public EditNameWindow()
        {
            InitializeComponent();
        }

        public void Initialize(CheckBox checkBoxInMain)
        {
            this.checkBoxInMain = checkBoxInMain;
            ((TextBox)this.FindName("EditNameTextbox")).Text = (string)checkBoxInMain.Content;
            ((TextBox)this.FindName("EditNameTextbox")).Focus();
        }

        public void Submit ()
        {
            checkBoxInMain.Content = (string)((TextBox)this.FindName("EditNameTextbox")).Text;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }
    }
}
