using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.Databases
{
    public class Login
    {
        // Variables
        string path;
        SQLiteConnection conn;

        public Login()
        {
            // Creating SQLite Database for user login information
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,
                "db.sqlite");
            conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            //Create Table
            conn.CreateTable<User>();
            
        }

        public void Register(User user)
        {
            // Inserting users username, password and e-mail to database
                conn.Insert(new User()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email
                });
        }

        // Boolean to check if username and password is true or false
        public bool LoginUser (string user, string password)
        {
            // Checking username and password is correct
            var query = conn.Table<User>().Where(s => s.UserName == user && s.Password == password);
            // If there is both a username and password that are correct it counts them and sees if they are = to 2
            if (query.Count() == 2)
                return true;
            else
                return false;
        }


    }
}
