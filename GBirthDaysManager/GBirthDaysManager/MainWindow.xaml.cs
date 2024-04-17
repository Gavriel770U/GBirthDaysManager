using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GBirthDaysManager
{
    public struct BirthDayData
    {
        public BirthDayData(string name, string date)
        {
            this.name = name;
            this.date = date;
        }

        public string name {  get; set; }
        public string date { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string loggedUserPath;
        private List<BirthDayData> _datalist;

        public MainWindow()
        {
            this._datalist = new List<BirthDayData>();

            LoginScreen ls = new LoginScreen();
            ls.ShowDialog();

            StreamReader streamReader = null;

            if (ls.passedLogin)
            {
                InitializeComponent();
                this.loggedUserPath = ".\\data\\"+ls.username+".txt";

                if (!File.Exists(this.loggedUserPath))
                {
                    File.Create(this.loggedUserPath).Dispose();
                }
                else
                {
                    streamReader = new StreamReader(this.loggedUserPath);
                    string line = "";

                    while (line != null)
                    {
                        line = streamReader.ReadLine();
                        if (null == line)
                        {
                            break;
                        }

                        string[] data = line.Split(',');

                        this._datalist.Add(new BirthDayData(data[0], data[1]));
                    }
                }

                CustomizeCalendar();
                
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

        private void CustomizeCalendar()
        {
            // Subscribe to the DisplayDateChanged event of the Calendar control
            Calendar.DisplayDateChanged += Calendar_DisplayDateChanged;
            // Subscribe to the Loaded event of the Calendar control
            Calendar.Loaded += Calendar_Loaded;
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            // Find the CalendarDayButton elements within the Calendar
            FindCalendarDayButtons(Calendar);
        }

        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            // Find the CalendarDayButton elements within the Calendar
            FindCalendarDayButtons(Calendar);
        }

        private void FindCalendarDayButtons(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is CalendarDayButton dayButton)
                {
                    // Get the date of the button
                    DateTime date = (DateTime)dayButton.DataContext;

                    if (date != DateTime.Today)
                    {
                        // Reset background color to white
                        dayButton.Background = Brushes.White;
                    }

                    // Check if it's the specific date you want to highlight
                    foreach (BirthDayData birthDayData in this._datalist)
                    {
                        if (date == DateTime.Parse(birthDayData.date)) // Change this to your specific date
                        {
                            // Change background color
                            dayButton.Background = Brushes.Yellow;
                        }
                    } 
                }
                else
                {
                    // Recursively search for CalendarDayButton elements
                    FindCalendarDayButtons(child);
                }
            }
        }
    }
}