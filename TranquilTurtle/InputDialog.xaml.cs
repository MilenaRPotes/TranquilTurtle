﻿using System.Windows;

namespace TranquilTurtle
{
    
    public partial class InputDialog : Window
    {
        public string ResponseText {  get; private set; }
        public InputDialog(string currentText)
        {
            InitializeComponent();
            InputTextBox.Text = currentText;
            InputTextBox.Focus();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseText = InputTextBox.Text;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
