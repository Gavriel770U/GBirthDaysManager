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

                    streamReader.Close();
                }

                this.CustomizeCalendar();
                
            }
            else
            {
                this.Close();
            }
        }

        private void AddCelebratorButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = this.DatePicker.SelectedDate;
            if (null == date)
            {
                MessageBox.Show("No date selected!", "GBirthDaysManager | Error", MessageBoxButton.OK, icon: MessageBoxImage.Error);
                return;
            }

            string celebratorName = this.AddCelebratorTextBox.Text.Trim();

            if (!string.IsNullOrWhiteSpace(celebratorName))
            {
                BirthDayData birthDayData = new BirthDayData(celebratorName, date.Value.ToShortDateString());
                this._datalist.Add(birthDayData);

                StreamWriter streamWriter = null;

                streamWriter = new StreamWriter(this.loggedUserPath, append: true);
                streamWriter.WriteLine(birthDayData.name + "," + birthDayData.date);

                streamWriter.Close();

                this.DatePicker.SelectedDate = null;
                this.AddCelebratorTextBox.Clear();

                this.FindCalendarDayButtons(this.Calendar);
            }
            else
            {
                MessageBox.Show("Name field cannot be empty or space!", "GBirthDaysManager | Error", MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }

        private void CustomizeCalendar()
        {
            // Subscribe to the DisplayDateChanged event of the Calendar control
            Calendar.DisplayDateChanged += Calendar_DisplayDateChanged;
            // Subscribe to the Loaded event of the Calendar control
            Calendar.Loaded += Calendar_Loaded;
            // Subscribe to the DisplayModeChanged event of the Calendar control
            Calendar.DisplayModeChanged += Calendar_DisplayModeChanged;
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

        private void Calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            // Reapply highlighting when the display mode changes
            if (e.NewMode == CalendarMode.Month)
            {
                FindCalendarDayButtons(Calendar);
            }
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
                    // Reset background color to white
                    dayButton.Background = Brushes.White;


                    if (date == DateTime.Today)
                    {
                        dayButton.Background = Brushes.LightSlateGray;
                    }

                    // Check if it's the specific date you want to highlight
                    foreach (BirthDayData birthDayData in this._datalist)
                    {
                        DateTime parsedDate = DateTime.Parse(birthDayData.date);
                        if (date.Month == parsedDate.Month && date.Day == parsedDate.Day) // Change this to your specific date
                        {
                            // Change background color
                            dayButton.Background = Brushes.LightSkyBlue;
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

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.CelebratorsLabel.Content = "";

            foreach (BirthDayData birthDayData in this._datalist)
            {
                DateTime selectedDate = this.Calendar.SelectedDate.Value;
                DateTime date = DateTime.Parse(birthDayData.date);

                if (selectedDate.Month == date.Month && selectedDate.Day == date.Day)
                {
                    this.CelebratorsLabel.Content += birthDayData.name + "\n";
                }
            }
        }
    }
}