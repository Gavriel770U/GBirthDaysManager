using System;
using System.IO;
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
using System.Windows.Shapes;
using System.Reflection;

namespace GBirthDaysManager
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public bool passedLogin = false;

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = this.UsernameTextBox.Text;
            string password = this.PasswordTextBox.Text;
            MessageBoxResult messageBoxResult = MessageBoxResult.None;

  
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(".\\data\\Users.txt");
                string line = "";
                
                while (line != null)
                {
                    line = streamReader.ReadLine();
                    if (null == line)
                    {
                        break;
                    }

                    string[] data = line.Split(',');

                    if (data[0].Equals(username) && data[1].Equals(password))
                    {
                        this.passedLogin = true;
                    }
                    else
                    {
                        this.passedLogin = false;
                    }
                }
            }
            catch (Exception ex)
            {
                messageBoxResult = MessageBox.Show("Users.txt file does not exist!", "GBirthDaysManager | Error", MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }

            if (MessageBoxResult.OK == messageBoxResult)
            {
                this.passedLogin = false;
                this.Close();
            }

            if (this.passedLogin)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentials!", "GBirthDaysManager | Error", MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.passedLogin = false;
            this.Close();
        }
    }
}
