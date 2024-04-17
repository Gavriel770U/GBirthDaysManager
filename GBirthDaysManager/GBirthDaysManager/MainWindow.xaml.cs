using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GBirthDaysManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string loggedUserPath;

        public MainWindow()
        {
            LoginScreen ls = new LoginScreen();
            ls.ShowDialog();

            if (ls.passedLogin)
            {
                InitializeComponent();
                this.loggedUserPath = "./data/"+ls.username+".txt";

                if (!File.Exists(this.loggedUserPath))
                {
                    File.Create(this.loggedUserPath).Dispose();
                }
                else
                {

                }
            }
            else
            {
                this.Close();
            }
        }

        private void AddCelebratorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.AddCelebratorTextBox.Text))
            {
                this.NewCelebratorLabel.Content = this.AddCelebratorTextBox.Text;
                this.AddCelebratorTextBox.Clear();
            }
        }
    }
}