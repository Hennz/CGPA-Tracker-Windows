using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Storage;
using Windows.UI.Popups;
using System.Net.Http;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;
using System.Xml.Linq;
namespace CGPA
{
    public sealed partial class Know : CGPA.Common.LayoutAwarePage
    {
        public Know()
        {
            this.InitializeComponent();
            read(); read1();
            Progress.IsActive = false;
        }
        private void OnAdError(object sender, Microsoft.Advertising.WinRT.UI.AdErrorEventArgs e)
        {
            CheckBox.IsChecked = true;
            switch (e.ErrorCode)
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
        private void ShowPopupAnimationClicked(String s)
        {
            if (!LightDismissAnimatedPopup.IsOpen)
            {
                errortext.Text = s;
                LightDismissAnimatedPopup.IsOpen = true;
            }
        }
        private void CloseAnimatedPopupClicked(object sender, RoutedEventArgs e)
        {
            if (LightDismissAnimatedPopup.IsOpen) { LightDismissAnimatedPopup.IsOpen = false; }
        }
        private void pop(String s)
        {
            ShowPopupAnimationClicked("OOPS :( :( :(\n" + s);
        }
        private async void read()
        {
            float cg = 0.0f;
            int credits = 0;
            float tgp = 0.0f;
            int tcredits = 0;
            Boolean newq = false;
            for (int i = 0; i < 12; i++)
            {
                String t1 = Text1.Text;
                String t2 = Text2.Text;
                String t3 = Text3.Text;
                String t4 = Text4.Text;
                try
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile storageFile = await localFolder.CreateFileAsync("sgpa" + i + ".txt", CreationCollisionOption.OpenIfExists);
                    Stream readStream = await storageFile.OpenStreamForReadAsync();
                    using (StreamReader reader = new StreamReader(readStream))
                    {
                        String text = reader.ReadLine();
                        if (text == null) { Text1.Text = t1 + "\n" + (i + 1); Text2.Text = t2 + "\nNo Data"; Text3.Text = t3 + "\nNo Data"; Text4.Text = t4 + "\nNo Data"; continue; }
                        else
                        {
                            newq = true;
                            cg = (Convert.ToSingle(text));
                            text = reader.ReadLine();
                            credits = (Convert.ToInt32(text));
                            tgp = tgp + (cg * credits);
                            tcredits += credits;
                            t1 += "\n" + (i + 1); t2 += "\n" + cg * credits; t3 += "\n" + credits; t4 += "\n" + cg.ToString("0.00");
                        }
                    }
                }
                catch (Exception) { TextBlock1.Text = "exce"; t1 += "\n" + (i + 1); t2 += "\n"; t3 += "\n"; t4 += "\n"; continue; }
                Text1.Text = t1; Text2.Text = t2; Text3.Text = t3; Text4.Text = t4;
            }
            if(newq)TextBlock1.Text = "Sum of total grade points are " + tgp + "\nSum of total credits is " + tcredits*10 + "\nYour current CGPA is\n" + (tgp / tcredits).ToString("0.00");
        }
        private async void read1()
        {
            StorageFile image = UserInformation.GetAccountPicture(AccountPictureKind.SmallImage) as StorageFile;
            if (image != null)
            {
                try
                {
                    IRandomAccessStream imageStream = await image.OpenReadAsync();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(imageStream);
                    welimg.Source = bitmapImage;

                }
                catch (Exception) { }
            }
            try { welcome.Text = "Hello, " + await UserInformation.GetDisplayNameAsync(); welcome.Text = "Hello, " + await UserInformation.GetFirstNameAsync(); }
            catch (Exception) { welcome.Text = "Hello"; } 
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("main.txt", CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String text = reader.ReadLine();
                    if (text == null) { Theme.SelectedIndex = 0; return; }
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
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Ad2.Visibility = Visibility.Collapsed;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Ad2.Refresh();
            Ad2.Visibility = Visibility.Visible;
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
            catch (Exception) { readimg(); }
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
            Text1.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            Text2.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            Text3.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            Text4.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            TextBlock5.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
            switch (ch)
            {
                case 1: { Grid.Background = new SolidColorBrush(Windows.UI.Colors.White);
                Themet.Foreground = new SolidColorBrush(Windows.UI.Colors.Black); return; }
                case 0:
                    {
                        Grid.Background = new SolidColorBrush(Windows.UI.Colors.Black); 
                        Text1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        Text2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        Text3.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        Text4.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        TextBlock5.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                        return;
                    }
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
        
    }
}
