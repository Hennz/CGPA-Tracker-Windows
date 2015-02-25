using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using System.Xml.Linq;
using Windows.Networking.BackgroundTransfer;
using System.Net.Http;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.Storage.Pickers;
namespace CGPA
{
     public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            grid1.Margin = new Thickness(90, 150, 0, 0);
            grid3.Margin = new Thickness(926, 150, 0, 0);
            read(); 
            Progress.IsActive = false;
        }
        private void grid1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(gpa));
                }
        }
        private void grid2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(sgpa));
            }
        }
        private void grid3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(scale));
            }
        }
        private void grid4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            change(); 
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(profile));
            }
        }
        private void grid5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            change(); 
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(about));
            }
        }
        private void Grid_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Point pos = e.GetCurrentPoint(this).Position;
            if (((pos.Y - 393.0) * (620-663) > (pos.X - 663.0) * (309-393)) && (((pos.Y - 393.0) * (579-663) > (pos.X - 663.0) * (434 - 393)))) { Image.Source = new BitmapImage(new Uri(this.BaseUri, "Assets/maing.png")); }
            if (((pos.Y - 393.0) * (620 - 663) > (pos.X - 663.0) * (309 - 393)) && (((pos.Y - 393.0) * (579 - 663) < (pos.X - 663.0) * (434 - 393)))) { Image.Source = new BitmapImage(new Uri(this.BaseUri, "Assets/mainp.png")); }
            if (((pos.Y - 393.0) * (620 - 663) < (pos.X - 663.0) * (309 - 393)) && (((pos.Y - 393.0) * (579 - 663) > (pos.X - 663.0) * (434 - 393)))) { Image.Source = new BitmapImage(new Uri(this.BaseUri, "Assets/mainc.png")); }
            if (((pos.Y - 393.0) * (620 - 663) < (pos.X - 663.0) * (309 - 393)) && (((pos.Y - 393.0) * (579 - 663) < (pos.X - 663.0) * (434 - 393)))) { Image.Source = new BitmapImage(new Uri(this.BaseUri, "Assets/maina.png")); }
        }
        private void OnAdError(object sender, Microsoft.Advertising.WinRT.UI.AdErrorEventArgs e)
        {
            CheckBox.IsChecked = true;
            switch(e.ErrorCode)
            {
                case MicrosoftAdvertising.ErrorCode.NoAdAvailable:
                     AdsText.Text = "No ad is available."; break; 
                case MicrosoftAdvertising.ErrorCode.NetworkConnectionFailure:
                     AdsText.Text = "A connection to the network could not be established."; break; 
                default:
                     AdsText.Text = "An unknown error occured."; break;
            }
            AdsText.Visibility = Visibility.Visible;
        }
        private async void read()
        {
            StorageFile image = UserInformation.GetAccountPicture(AccountPictureKind.SmallImage) as StorageFile;
            if (image != null)
            {
                try
                {
                    IRandomAccessStream imageStream = await image.OpenReadAsync();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(imageStream);
                    BITS.Source = bitmapImage;
                }
                catch (Exception) { }
            }
            String dn="Guest";
            try{ dn = await UserInformation.GetDisplayNameAsync(); }
            catch (Exception) { dn = "Guest"; } 
            try { dn = await UserInformation.GetFirstNameAsync(); }
            catch (Exception) { dn = "Guest"; }
            Welcome.Text = "Welcome, " + dn;
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("main.txt", CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String text = reader.ReadLine();
                    if (text == null){ Theme.SelectedIndex = 0; return; }
                    int t = Convert.ToInt32(text);
                    Theme.SelectedIndex = t;
                }
            }
            catch (Exception) { Theme.SelectedIndex = 0; }
        }
        private async void change()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("main.txt", CreationCollisionOption.OpenIfExists);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine(Theme.SelectedIndex.ToString());
                }
            }
            catch (Exception) { }
        }
        private async void setimg()
        {
            try
            {
                System.Xml.Linq.XDocument xmlDoc = XDocument.Load("http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US");
                IEnumerable<string> strTest = from node in xmlDoc.Descendants("url") select node.Value;
                string strURL = "http://www.bing.com" + strTest.First();
                Uri source = new Uri(strURL);
                var bitmapImage = new BitmapImage();
                var httpClient = new HttpClient();
                var httpResponse = await httpClient.GetAsync(source);
                byte[] b = await httpResponse.Content.ReadAsByteArrayAsync();
                using (var stream = new InMemoryRandomAccessStream())
                {
                    using (DataWriter dw = new DataWriter(stream))
                    {
                        dw.WriteBytes(b);
                        await dw.StoreAsync();
                        stream.Seek(0);
                        bitmapImage.SetSource(stream);
                        Image1.Source = bitmapImage;
                        var storageFile = await KnownFolders.PicturesLibrary.CreateFileAsync("CGPA_Bing.jpg", CreationCollisionOption.ReplaceExisting);
                        using (var storageStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            await RandomAccessStream.CopyAndCloseAsync(stream.GetInputStreamAt(0), storageStream.GetOutputStreamAt(0));
                        }
                    }
                }
            }
            catch (Exception) { readimg();
            }
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
        private async void browseimg()
        {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            open.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types
            open.FileTypeFilter.Clear();
            open.FileTypeFilter.Add(".bmp");
            open.FileTypeFilter.Add(".png");
            open.FileTypeFilter.Add(".jpeg");
            open.FileTypeFilter.Add(".jpg");

            // Open a stream for the selected file
            StorageFile file = await open.PickSingleFileAsync();

            // Ensure a file was selected
            if (file != null)
            {
                // Ensure the stream is disposed once the image is loaded
                using (IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);
                    Image1.Source = bitmapImage;
                    
                }
                try { await file.CopyAsync(KnownFolders.PicturesLibrary, "CGPA_Browse.jpg", NameCollisionOption.ReplaceExisting); }
                catch (Exception) { }
            }
        }
        private void ComboBoxItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            browseimg();
        }
        private void themec(int ch)
        {
            Image1.Visibility = Visibility.Collapsed;
            change();
            Themet.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            switch (ch)
            {
                case 1: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.White); Themet.Foreground = new SolidColorBrush(Windows.UI.Colors.Black); return; }
                case 0: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Black); return; }
                case 2: { Image1.Visibility = Visibility.Visible; setimg(); return; }
                case 3: { Image1.Visibility = Visibility.Visible; readbimg(); return; }
                case 4: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Green); return; }
                case 5: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Blue); return; }
                case 6: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Red); return; }
                case 7: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Pink); return; }
                case 8: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Yellow); return; }
                case 9: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Brown); return; }
                case 10: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Orange); return; }
                case 11: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Purple); return; }
                case 12: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Silver); return; }
                case 13: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.Gold); return; }
            }
        }
        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Theme.SelectedIndex == 14)
                {
                    Random rnd = new Random();
                    int th = rnd.Next(0, 14);
                    themec(th);
                }
                else
                {
                    themec(Theme.SelectedIndex);
                }
            }
            catch (Exception) { }
        }
        private void grid1_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border1.Visibility = Visibility.Visible; border1.BorderThickness = t1;
        }
        private void grid1_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border1.Visibility = Visibility.Collapsed;
        }
        private void grid2_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border2.Visibility = Visibility.Visible; border2.BorderThickness = t1;
            
        }
        private void grid2_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border2.Visibility = Visibility.Collapsed; 
        }
        private void grid3_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border3.Visibility = Visibility.Visible; border3.BorderThickness = t1;
        }
        private void grid3_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border3.Visibility = Visibility.Collapsed; 
        }
        private void grid4_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border4.Visibility = Visibility.Visible; border4.BorderThickness = t1;
        }
        private void grid4_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border4.Visibility = Visibility.Collapsed;
        }
        private void grid5_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border5.Visibility = Visibility.Visible; border5.BorderThickness = t1;
        }
        private void grid5_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border5.Visibility = Visibility.Collapsed; 
        }
        Thickness t = new Thickness(10.0);
        Thickness t1 = new Thickness(5.0);
        private void grid1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            border1.BorderThickness = t;
        }
        private void grid2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            border2.BorderThickness = t;
        }
        private void grid3_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            border3.BorderThickness = t; 
        }
        private void grid4_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            border4.BorderThickness = t;
        }
        private void grid5_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            border5.BorderThickness = t; 
        }
        private void grid1_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            border1.BorderThickness = t; 
        }
        private void grid3_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            border3.BorderThickness = t; 
        }
        private void grid4_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            border4.BorderThickness = t; 
        }
        private void grid2_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            border2.BorderThickness = t; 
        }
        private void grid5_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            border5.BorderThickness = t;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(gpa));
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(sgpa));
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(scale));
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(profile));
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            change();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(about));
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Ad1.Visibility = Visibility.Collapsed;
            Ad2.Visibility = Visibility.Collapsed;
            Ad3.Visibility = Visibility.Collapsed;
            grid1.Margin = new Thickness(90, 150, 0, 0);
            grid3.Margin = new Thickness(926, 150, 0, 0);
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Ad1.Visibility = Visibility.Visible;
            Ad2.Visibility = Visibility.Visible;
            Ad3.Visibility = Visibility.Visible;
            grid1.Margin = new Thickness(160, 150, 0, 0);
            grid3.Margin = new Thickness(856, 150, 0, 0);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(login));
            }
        }
        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Uri uri = new Uri("http://srujanjha.wix.com/cgpa-tracker");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        private async void Image_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Uri uri = new Uri("http://bestwindows8apps.net/cgpa-tracker/");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
