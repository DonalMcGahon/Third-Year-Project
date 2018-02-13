using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Agenda.Databases;
using Windows.ApplicationModel.Appointments;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Agenda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListsPages : Page
    {
        // Variables
        #region
        string path;
        string path2;
        string path3;
        SQLite.Net.SQLiteConnection conn;
        SQLite.Net.SQLiteConnection conn2;
        SQLite.Net.SQLiteConnection conn3;
        #endregion

        //public CalendarClass cal = new CalendarClass();

        public ListsPages()
        {
            this.InitializeComponent();

            //Link to setting up databses - https://www.tutorialspoint.com/windows10_development/windows10_development_sqlite_database.htm
            // SQLite Databases Set-up.
            #region
            // SQLite for Task Database
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
            "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            
            conn.CreateTable<Database>();
            // This displays the data inside of the app when the app is opened
            myData.ItemsSource = conn.Table<Database>();

            // SQLite for Grocery List
            path2 = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
            "db.sqlite");
            conn2 = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path2);


            conn2.CreateTable<gDatabase>();
            // This displays the data inside of the app when the app is opened
            groceryData.ItemsSource = conn2.Table<gDatabase>();


            // SQLite for Exercise List
            path3 = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
            "db.sqlite");
            conn3 = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path3);


            conn3.CreateTable<eDatabase>();
            // This displays the data inside of the app when the app is opened
            exerciseData.ItemsSource = conn3.Table<eDatabase>();
            
        }
        #endregion

        // Delete Single Entry of Data in To-Do Database
        #region
        private void button6_Click(object sender, RoutedEventArgs e)
        {
            // Select the Id, Name and Date from the database.
            var entry = conn.Query<Database>("select Id, Name, Date from Database").FirstOrDefault();
            
            // Delete Entry
            conn.Delete(entry);

            // Updates Database
            myData.ItemsSource = conn.Table<Database>();
            
        }
        #endregion

        // Delete Last Entry entered into the To-Do Database
        #region
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            // Select the Id, Name and Date from the database.
            var entry = conn.Query<Database>("select Id, Name, Date from Database").Last();

            // Delete Entry
            conn.Delete(entry);

            // Updates Database
            myData.ItemsSource = conn.Table<Database>();
        }
        #endregion

        // Delete TO-DO Data
        #region
        private void button5_Click(object sender, RoutedEventArgs e)
        {
            // Deletes Whole Database
            conn.DropTable<Database>();

            // Create New Database
            conn.CreateTable<Database>();

            // Updates Database
            myData.ItemsSource = conn.Table<Database>();

        }
        #endregion

        // Button to add To-Do tasks to database
        #region
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var s = conn.Insert(new Database()
            {
                Name = textBox.Text,
                Date = textBox_Copy.Text

            });
            // Updates the ItemsSource for ListView
            myData.ItemsSource = conn.Table<Database>();
            // Empty String in textboxes once user clicks add button
            textBox.Text = String.Empty;
            textBox_Copy.Text = String.Empty;
        }
        #endregion

        // Calendar Button
        #region
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            calendar.IsCalendarOpen = false;
            textBox_Copy.Text = args.NewDate?.DateTime.ToString("dd/MM/yyyy");
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            calendar.IsCalendarOpen = true;
        }
        #endregion

        // Relevant link to Grocery AutoSuggestions - https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/auto-suggest-box
        // Grocery List Buttons and Textboxs etc.
        #region
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // If user enters text go into this if statement
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // If user enters text with length greater than 0
                if (sender.Text.Length > 0)
                {
                    sender.ItemsSource = new string[] { "asparagus", "apples", "avacado", "alfalfa", "acorn squash", "almond", "arugala", "artichoke", "applesauce", "asian noodles", "antelope", "ahi tuna", "albacore tuna", "Apple juice", "Avocado roll", "Bruscetta", "bacon", "black beans", "bagels", "baked beans", "BBQ", "bison", "barley", "beer", "bisque", "bluefish", "bread", "broccoli", "buritto", "babaganoosh", "Cabbage", "cake", "carrots", "carne asada", "celery", "cheese", "chicken", "catfish", "chips", "chocolate", "chowder", "clams", "coffee", "cookies", "corn", "cupcakes", "crab", "curry", "cereal", "chimichanga", "dates", "dips", "duck", "dumplings", "donuts", "eggs", "enchilada", "eggrolls", "English muffins", "edimame", "eel sushi", "fajita", "falafel", "fish", "franks", "fondu", "French toast", "French dip", "Garlic", "ginger", "gnocchi", "goose", "granola", "grapes", "green beans", "Guancamole", "gumbo", "grits", "Graham crackers", "ham", "halibut", "hamburger", "honey", "huenos rancheros", "hash browns", "hot dogs", "haiku roll", "hummus", "ice cream", "Irish stew", "Indian food", "Italian bread", "jambalaya", "jelly", "jam", "jerky", "jalapeño", "kale", "kabobs", "ketchup", "kiwi", "kidney beans", "kingfish", "lobster", "Lamb", "Linguine", "Lasagna", "Meatballs", "Moose", "Milk", "Milkshake", "Noodles", "Ostrich", "Pizza", "Pepperoni", "Porter", "Pancakes", "Quesadilla", "Quiche", "Reuben", "Spinach", "Spaghetti", "Tater tots", "Toast", "Venison", "Waffles", "Wine", "Walnuts", "Yogurt", "Ziti", "Zucchini" };

                }
                // Otherwise if less than 1
                else
                {
                    sender.ItemsSource = new string[] { "No results" };
                }
            }
        }
        #endregion

        // Textbox for the amount of groeries you want
        #region
        private void textBox9_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        // Button adding to Grocery List
        #region
        private void done_Click(object sender, RoutedEventArgs e)
        {
            var g = conn2.Insert(new gDatabase()
            {
                gName = AddGrocery.Text,
                gAmount = textBox9.Text

            });
            // Updates the ItemsSource for ListView
            groceryData.ItemsSource = conn2.Table<gDatabase>();
            // Empty String in textboxes once user clicks add button
            AddGrocery.Text = String.Empty;
            textBox9.Text = String.Empty;
        }
        #endregion

        // Delete First Grocery Item
        #region
        private void button7_Click(object sender, RoutedEventArgs e)
        {
            // Select the Id, Name and Date from the database.
            var entry = conn2.Query<gDatabase>("select gId, gName, gAmount from gDatabase").FirstOrDefault();

            // Delete Entry
            conn2.Delete(entry);

            // Updates Database
            groceryData.ItemsSource = conn2.Table<gDatabase>();
        }
        #endregion

        // Delete Last Grocery Item
        #region
        private void button10_Click(object sender, RoutedEventArgs e)
        {
            // Select the Id, Name and Date from the database.
            var entry = conn2.Query<gDatabase>("select gId, gName, gAmount from gDatabase").Last();

            // Delete Entry
            conn2.Delete(entry);

            // Updates Database
            groceryData.ItemsSource = conn2.Table<gDatabase>();
        }
        #endregion

        // Delete Grocery List
        #region
        private void groceryDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete Whole Database
            conn2.DropTable<gDatabase>();

            // Create New Database
            conn2.CreateTable<gDatabase>();

            // Updates Database
            groceryData.ItemsSource = conn2.Table<gDatabase>();
        }
        #endregion

        // End Grocery Buttons and Textboxs etc.

        // Exercise List Buttons and Textboxs etc.
        #region
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        // Delete First Single Exercises
        #region
        private void button8_Click(object sender, RoutedEventArgs e)
        {
            // Select all entries entered into the database.
            var entry = conn3.Query<eDatabase>("select * from eDatabase").FirstOrDefault();

            // Delete Entry
            conn3.Delete(entry);

            // Updates Database
            exerciseData.ItemsSource = conn3.Table<eDatabase>();
        }
        #endregion

        // Delete Last Exercise
        #region
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            // Select all entries entered into the database.
            var entry = conn3.Query<eDatabase>("select * from eDatabase").Last();

            // Delete Entry
            conn3.Delete(entry);

            // Updates Database
            exerciseData.ItemsSource = conn3.Table<eDatabase>();
        }
        #endregion

        // Delete Exercises Button
        #region
        private void delete_Exercises_Click(object sender, RoutedEventArgs e)
        {
            // Delete Whole Database
            conn3.DropTable<eDatabase>();

            // Create New Database
            conn3.CreateTable<eDatabase>();

            // Updates Database
            exerciseData.ItemsSource = conn3.Table<eDatabase>();
        }
        #endregion

        // Button to add Exercises + Reps to Database
        #region
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var r = conn3.Insert(new eDatabase()
            {
                ex1 = textBox1.Text,
                ex2 = textBox2.Text,
                ex3 = textBox3.Text,

                rep1 = textBox5.Text,
                rep2 = textBox6.Text,
                rep3 = textBox7.Text,

            });
            // Updates the ItemsSource for ListView
            exerciseData.ItemsSource = conn3.Table<eDatabase>();
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
        }
        #endregion


        // Relevant link to Exercise AutoSuggestions - https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/auto-suggest-box
        // Exercise tracker Buttons and Textboxs etc.
        #region
        private void AutoSuggestBox_TextChanged1(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // If user enters text go into this if statement
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // If user enters text with length greater than 0
                if (sender.Text.Length > 0)
                {
                    sender.ItemsSource = new string[] { "Crunch", "Resisted Crunch", "Inclined Crunch with Feet Attached", "Crunch with Leg Curl", "Sit - Up with Feet Attached", "Sit - Up with Cable", "Trunk Rotation", "Jacknife Sit - Up", "High Leg Pull - In", "Low Leg Pull - In", "Side Plank", "Hyperextension", "Row", "Crossover Row", "Kneeling Row", "Row with Hyperextension", "Back Fly", "Rotating Back Fly", "Prone Back Fly", "Back Fly with Leg Curl", "Lateral Pulldown", "Pulldown with Squat with Elbows Flexed", "Lateral Pulldown with Squat", "Pull - Up", "Chest Press", "Close - Grip Chest Press", "Wide - Grip Chest Press", "Incline Push Up", "Chest Fly", "Incline Chest Fly", "Decline Chest Fly", "Lateral Chest Fly", "Pullover", "Pullover with Crunch", "Pullover with Twisting Crunch", "Pullover with Squat", "Single Leg Pullover with Squat", "Leg Curl", "Reverse Leg Curl", "Squat", "Wide Squat", "Single - Leg Squat", "Single - Leg Squat On Side", "Single - Leg Squat Kneeling", "Jumping Squat", "Twisting Squat", "Jumping and Twisting Squat", "Front Lunge", "Hip Extension with Knee Stabilized", "Hip Abduction", "Hip Adduction", "Calf Raise", "Shoulder Press", "Upright Row", "Upright Row with Hyperextension", "Upright Row with Leg Curl", "Lateral Deltoid Raise", "Front Deltoid Raise", "Wide Grip Front Deltoid Raise", "Front Deltoid Raise with Supination Grip", "Lying Front Deltoid Raise", "Front Deltoid Raise with Leg Curl", "Shoulder Extension", "Shoulder Extension with Hyperextension", "Shoulder Extension with Leg Curl", "Lateral Arm Pull", "Biceps Curl", "Lateral Single - Arm Biceps Curl", "Lying Biceps Curl", "Biceps Curl with Crunching", "Biceps Curl with Hyperextension", "Biceps Curl with Leg Curl", "Chin - Up", "Triceps Extension", "Prone Triceps Extension", "Kneeling Triceps Extension", "Lateral Single - Arm Triceps Extension" };
                    
                }
                // Otherwise if less than 1
                else
                {
                    sender.ItemsSource = new string[] { "No results" };
                }
            }
        }
        #endregion

        
        // Calendar Region
        #region
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            // Reference link for code below, for adding events - http://stackoverflow.com/questions/38355922/system-argumentexception-in-app-when-accepting-appointment

            var rect = new Rect(new Point(Window.Current.Bounds.Width / 2, Window.Current.Bounds.Height / 2), new Size());
            DateTimeOffset date = StartDate.Date;
            TimeSpan time = StartTime.Time;
            int minutes = int.Parse((string)((ComboBoxItem)Duration.SelectedItem).Tag);

            // Creating a new Event
            Appointment newEvent = new Appointment()
            {
                // Creating a date and time values and getting the current date from OS using TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)
                Subject = Subject.Text,
                Location = Location.Text,
                Details = Details.Text,
                StartTime = new DateTimeOffset(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0, TimeZoneInfo.Local.GetUtcOffset(DateTime.Now)),
                Duration = TimeSpan.FromMinutes(minutes)
            };
            // The following line triggers the launch of the calendar app for the user to confirm adding the appointment
            string id = await AppointmentManager.ShowAddAppointmentAsync(newEvent, rect, Placement.Default);

            // Empties all data entered
            StartDate.Date = DateTime.Now;
            StartTime.Time = DateTime.Now.TimeOfDay;
            Subject.Text = string.Empty;
            Details.Text = string.Empty;
            Location.Text = string.Empty;
            Duration.SelectedIndex = 0;

    }

        private async void Calendar_Click(object sender, RoutedEventArgs e)
        {
            // Opens Calendar App on the Current Date and Time
            await AppointmentManager.ShowTimeFrameAsync(StartDate.Date, StartTime.Time);
        }
        #endregion

        // Calendar Sign Out Button
        #region
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        #endregion
    }
}