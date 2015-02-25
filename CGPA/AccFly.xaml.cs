using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using Microsoft.Live;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CGPA
{
    public sealed partial class AccFly : UserControl
    {
        public AccFly()
        {
            this.InitializeComponent();
            SetNameField(false); 
            read();
        }
        private async void read()
        { 
        try
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.CreateFileAsync("main.txt", CreationCollisionOption.OpenIfExists);
            Stream readStream = await storageFile.OpenStreamForReadAsync();
            using (StreamReader reader = new StreamReader(readStream))
            {
                String text = reader.ReadLine();
                if (text == null) { themec(0); return; }
                int t = Convert.ToInt32(text);
                themec(t);
            }
        }
        catch (Exception) { themec(0); }
        }
        private async void readimg()
        {
            var bitmapImage = new BitmapImage();
            var pictureFile = await KnownFolders.PicturesLibrary.GetFileAsync("CGPA_Bing.jpg");
            using (var pictureStream = await pictureFile.OpenAsync(FileAccessMode.Read))
            {
                bitmapImage.SetSource(pictureStream);
            }
            Image1.Source = bitmapImage;
        }
        private async void readbimg()
        {
            try
            {
                var bitmapImage = new BitmapImage();
                var pictureFile = await KnownFolders.PicturesLibrary.GetFileAsync("CGPA_Browse.jpg");
                using (var pictureStream = await pictureFile.OpenAsync(FileAccessMode.Read))
                {
                    bitmapImage.SetSource(pictureStream);
                }
                Image1.Source = bitmapImage;
            }
            catch (Exception) { }
        }
        private void themec(int ch)
        {
            Image1.Visibility = Visibility.Collapsed;
            userName.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            switch (ch)
            {
                case 1: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Black); userName.Foreground = new SolidColorBrush(Windows.UI.Colors.White); return; }
                case 0: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Gray); return; }
                case 2: { Image1.Visibility = Visibility.Visible; readimg(); return; }
                case 3: { Image1.Visibility = Visibility.Visible; readbimg(); return; }
                case 4: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow); return; }
                case 5: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.BlueViolet); return; }
                case 6: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.DarkRed); return; }
                case 7: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.DeepPink); return; }
                case 8: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.YellowGreen); return; }
                case 9: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.SandyBrown); return; }
                case 10: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.OrangeRed); return; }
                case 11: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Violet); return; }
                case 12: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.LightSlateGray); return; }
                case 13: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.DarkGoldenrod); return; }
            }
        }
        private void AccountBackClicked(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
            {
                ((Popup)this.Parent).IsOpen = false;
            }
            SettingsPane.Show();
        }
        private async Task SetNameField(Boolean login)
        {
            // If login == false, just update the name field. 
            await App.updateUserName(this.userName, login);

            // Test to see if the user can sign out.
            Boolean userCanSignOut = true;

            LiveAuthClient LCAuth = new LiveAuthClient();
            LiveLoginResult LCLoginResult = await LCAuth.InitializeAsync();

            if (LCLoginResult.Status == LiveConnectSessionStatus.Connected)
            {
                userCanSignOut = LCAuth.CanLogout;
            }

            if (this.userName.Text.Equals("You're not signed in."))
            {
                // Show sign-in button.
                signInBtn.Visibility = Visibility.Visible;
                signOutBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Show sign-out button if they can sign out.
                signOutBtn.Visibility = (userCanSignOut ? Visibility.Visible : Visibility.Collapsed);
                signInBtn.Visibility = Visibility.Collapsed;
            }
        }
        private async void SignInClick(object sender, RoutedEventArgs e)
        {
            // This call will sign the user in and update the Account flyout UI.
            await SetNameField(true);
        }
        private async void SignOutClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Initialize access to the Live Connect SDK.
                LiveAuthClient LCAuth = new LiveAuthClient();
                LiveLoginResult LCLoginResult = await LCAuth.InitializeAsync();
                // Sign the user out, if he or she is connected;
                //  if not connected, skip this and just update the UI
                if (LCLoginResult.Status == LiveConnectSessionStatus.Connected)
                {
                    LCAuth.Logout();
                }

                // At this point, the user should be disconnected and signed out, so
                //  update the UI.
                this.userName.Text = "You're not signed in.";

                // Show sign-in button.
                signInBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
                signOutBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            catch (LiveConnectException)
            {
                // Handle exception.
            }
        }
        }
}
