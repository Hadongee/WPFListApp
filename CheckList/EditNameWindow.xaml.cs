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
        private bool closing = false;

        public EditNameWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(EditNameWindow_KeyDown);
            this.Deactivated += new EventHandler(EditNameWindow_Deactivated);
            this.Closing += new System.ComponentModel.CancelEventHandler(EditNameWindow_Closing);
        }

        // Key listener
        private void EditNameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (((TextBox)this.FindName("EditNameTextbox")).IsFocused)
                {
                    Submit();
                }
            }
        }

        // Deactivated Listener
        private void EditNameWindow_Deactivated (object sender, EventArgs e)
        {
            if (!closing)
            {
                this.Close();
            }
        }

        // Closing window listener
        // This is required for the deactivated listener so that it does not
        // try to close an already closing window
        private void EditNameWindow_Closing (object sender, EventArgs e)
        {
            closing = true;
        }

        // Called from the main window when you open this window
        public void Initialize(CheckBox checkBoxInMain)
        {
            // Select the CheckBox control which is being editted in the main window
            this.checkBoxInMain = checkBoxInMain;

            // The TextBox control in the editor window
            TextBox editTextBox = (TextBox)this.FindName("EditNameTextbox");

            // Set the text of the text box equal to the list's name
            editTextBox.Text = (string)checkBoxInMain.Content;
            editTextBox.Focus();
            editTextBox.SelectAll();
        }

        // Submit list element text change
        public void Submit ()
        {
            checkBoxInMain.Content = (string)((TextBox)this.FindName("EditNameTextbox")).Text;
            this.Close();
        }

        // Submit button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }
    }
}
