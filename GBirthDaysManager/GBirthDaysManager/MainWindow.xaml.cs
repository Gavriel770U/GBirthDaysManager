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
        public MainWindow()
        {
            LoginScreen ls = new LoginScreen();
            ls.ShowDialog();
            InitializeComponent();
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