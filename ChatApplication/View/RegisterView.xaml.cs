﻿using System.Windows.Controls;

namespace ChatClient.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void UsernameTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UsernameTB.Text.Length >= 1)
            {
                UsernameLabel.Content = "";
            }
            else
            {
                UsernameLabel.Content = "Username";
            }
        }

        private void EmailTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (EmailTB.Text.Length >= 1)
            {
                EmailLabel.Content = "";
            }
            else
            {
                EmailLabel.Content = "Email";
            }
        }

        private void PassTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PassTB.Text.Length >= 1)
            {
                PasswordLabel.Content = "";
            }
            else
            {
                PasswordLabel.Content = "Password";
            }
        }
    }
}
