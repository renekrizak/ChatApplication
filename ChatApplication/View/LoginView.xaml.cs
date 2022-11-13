using System.Windows.Controls;

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

            if (loginUsernameTB.Text.Length >= 1)
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
            if (loginPassTB.Text.Length >= 1)
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
