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
using Microsoft.Live;
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CGPA
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class login : Page
    {
        public login()
        {
            this.InitializeComponent();
        }
        private async void SignIn(object sender, RoutedEventArgs e)
        {
            try
            {
                LiveAuthClient auth = new LiveAuthClient();
                LiveLoginResult loginResult =
                    await auth.LoginAsync(new string[] { "wl.basic" });
                if (loginResult.Status == LiveConnectSessionStatus.Connected)
                {
                    this.infoTextBlock.Text = "Signed in.";
                }
            }
            catch (LiveAuthException exception)
            {
                this.infoTextBlock.Text = "Error signing in: "
                    + exception.Message;
            }
        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            LiveAuthClient auth = new LiveAuthClient();
            try { auth.Logout(); infoTextBlock.Text = "Logged Out"; }

            catch (Exception ec) { infoTextBlock.Text = ec.ToString(); }
        }
        private async void btnGreetUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LiveAuthClient auth = new LiveAuthClient();
                LiveLoginResult initializeResult = await auth.InitializeAsync();
                try
                {
                    LiveLoginResult loginResult = await auth.LoginAsync(new string[] { "wl.basic" });
                    if (loginResult.Status == LiveConnectSessionStatus.Connected)
                    {
                        LiveConnectClient connect = new LiveConnectClient(auth.Session);
                        LiveOperationResult operationResult = await connect.GetAsync("me");
                        dynamic result = operationResult.Result;
                        if (result != null)
                        {
                            this.infoTextBlock.Text = string.Join(" ", "Hello", result.name, "!");
                        }
                        else
                        {
                            this.infoTextBlock.Text = "Error getting name.";
                        }

                    }
                }
                catch (LiveAuthException exception)
                {
                    this.infoTextBlock.Text = "Error signing in: " + exception.Message;
                }
                catch (LiveConnectException exception)
                {
                    this.infoTextBlock.Text = "Error calling API: " + exception.Message;
                }
            }
            catch (LiveAuthException exception)
            {
                this.infoTextBlock.Text = "Error initializing: " + exception.Message;
            }
            
        }
      private async void btnDisplayProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LiveAuthClient auth = new LiveAuthClient();
                
                LiveConnectClient liveClient = new LiveConnectClient(auth.Session);
                LiveOperationResult operationResult = await liveClient.GetAsync("me/picture");
                dynamic result = operationResult.Result;
                BitmapImage image = new BitmapImage(new Uri(result.location, UriKind.Absolute));
                this.imgResult.Source = image;
            }
            catch (LiveConnectException exception)
            {
                this.infoTextBlock.Text = "Error getting user picture: " + exception.Message;
            }
        }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
          //Text1.Text = (Convert.ToInt32(t1.Text)+Convert.ToInt32(t2.Text)).ToString();
          }

      private void button_Click_1(object sender, RoutedEventArgs e)
      {
         button.Background = new SolidColorBrush(Windows.UI.Colors.Red);
            
      }

      private void Rectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
      {
          SolidColorBrush mySolidColorBrush = new SolidColorBrush();
          mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255,44, 66, 106);
          rectangle.Fill = mySolidColorBrush;          
      }

      private void Rectangle_PointerExited(object sender, PointerRoutedEventArgs e)
      {
          SolidColorBrush mySolidColorBrush = new SolidColorBrush();
          mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 56, 85, 136);
          rectangle.Fill = mySolidColorBrush;  
      }

      private void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
      {
          SolidColorBrush mySolidColorBrush = new SolidColorBrush();
          mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 78, 118, 191);
          rectangle.Fill = mySolidColorBrush;  
      }

      private void Rectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
      {
          SolidColorBrush mySolidColorBrush = new SolidColorBrush();
          mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 44, 66, 106);
          rectangle.Fill = mySolidColorBrush;  
      }



      public Brush Color1 { get; set; }
    }
}
