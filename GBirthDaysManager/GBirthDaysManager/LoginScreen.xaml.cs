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
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = this.UsernameTextBox.Text;
            string password = this.PasswordTextBox.Text;

  
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(".\\data\\Users.txt");
                string line;

                line = streamReader.ReadLine();                
                MessageBox.Show(line);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
