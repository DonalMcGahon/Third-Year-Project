using Agenda.Databases;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Agenda
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        Login db;

        public Register()
        {
            this.InitializeComponent();
            db = new Login();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Creating new user with their username, password and email
            db.Register(new User()
            {
                UserName = txtUser.Text,
                Password = txtPassword.Password,
                Email = txtEmail.Text
            });
            
                var message = new MessageDialog("Register Succeed");
                await message.ShowAsync();
                this.Frame.Navigate(typeof(MainPage));
            
        }
        // Return user Back to the Main Page
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
