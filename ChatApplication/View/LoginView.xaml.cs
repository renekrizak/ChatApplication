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

namespace ChatClient.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if(loginUsernameTB.Text.Length >= 1)
            {
                usernameLabel.Content = "";
            }
            else
            {
                usernameLabel.Content = "Username";
               
            }
        }

        private void loginPassTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(loginPassTB.Text.Length >= 1)
            {
                passwordLabel.Content = "";
            }
            else
            {
                passwordLabel.Content = "Password";
            }
        }
    }
}
