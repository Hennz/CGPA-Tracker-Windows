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
    public sealed partial class personald : CGPA.Common.LayoutAwarePage
    {
        Boolean newu=true;
        String pass = "";
        public personald()
        {
            this.InitializeComponent();
            read();
            read1();
            if (!newu)
            {
                TextBlock.Visibility = Visibility.Visible;
                Password.Visibility = Visibility.Visible;
            }
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
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
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
                    welimg.Source = bitmapImage;

                }
                catch (Exception) { }
            }
            try { welcome.Text = "Hello, " + await UserInformation.GetDisplayNameAsync(); welcome.Text = "Hello, " + await UserInformation.GetFirstNameAsync(); }
            catch (Exception) { welcome.Text = "Hello"; } 
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("Important.txt", CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String text = reader.ReadLine();
                    if (text == null) { newu = true; return; }
                    else
                    {
                        TextBlock.Visibility = Visibility.Visible;
                        Password.Visibility = Visibility.Visible;
                        newu = false;
                    }
                    TextBox1.Text = text;
                    text = reader.ReadLine();
                    TextBox2.Password = text;
                    pass = text;
                    text = reader.ReadLine();
                    TextBox3.Text = text;
                    text = reader.ReadLine();
                    TextBox4.Text = text;
                    text = reader.ReadLine();
                    TextBox5.Text = text;
                    text = reader.ReadLine();
                    TextBox6.Text = text;
                    text = reader.ReadLine();
                    TextBox7.Text = text;
                    text = reader.ReadLine();
                    TextBox8.Text = text;
                }
            }
            catch (Exception) { }
        }
        private Boolean check()
        {
            if (TextBox1.Text.Equals("E-Mail Id (User-name)") || TextBox1.Text == null) { pop("Your E-Mail ID isn't valid"); return false; }
            if (TextBox2.Password.Equals("Password") || TextBox2.Password == null)  { pop("Your Password isn't valid"); return false; }
            if (TextBox3.Text.Equals("First Name") || TextBox3.Text == null) { pop("Your First Name isn't valid"); return false; }
            if (TextBox4.Text.Equals("Last Name") || TextBox4.Text == null) { pop("Your Last Name isn't valid"); return false; }
            if (TextBox5.Text.Equals("I. D. Number/ Roll Number") || TextBox5.Text == null)  { pop("Your ID number isn't valid"); return false; }
            if (TextBox6.Text.Equals("Phone Number") || TextBox6.Text == null) { pop("Your Phone number isn't valid"); return false; }
            if (TextBox7.Text.Equals("Institute's Name") || TextBox7.Text == null) { pop("Your Institute's Name isn't valid"); return false; }
            if (TextBox8.Text.Equals("Branch Name / Class") || TextBox8.Text == null) { pop("Your Branch's Name isn't valid"); return false; }

            if (newu)
            {
                if (TextBox7.Text.Contains("BITS") || TextBox7.Text.Contains("bits") || TextBox7.Text.Contains("birla institute of technology & science") || TextBox7.Text.Contains("birla institute Of technology and science") ||
                    TextBox7.Text.Contains("Birla Institute Of Technology & Science") || TextBox7.Text.Contains("BIRLA INSTITUTE OF TECHNOLOGY AND SCIENCE") || TextBox7.Text.Contains("Birla Institute Of Technology And Science")||TextBox7.Text.Contains("BIRLA INSTITUTE OF TECHNOLOGY & SCIENCE"))
                {ShowPopupAnimationClicked("So, You Are a BITSIAN too.\n  :) :) :)"); }
                
                return false;
            }
            else
            {if (Password.Password.Equals(pass))
                    return false;
            else
            {
                pop("Password Doesn't Match!!!\n Access Denied"); Progress.IsActive = false;
                return true;
            }
            }
        }
        private async void finalise()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("Important.txt", CreationCollisionOption.OpenIfExists);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine(TextBox1.Text);
                    writer.WriteLine(TextBox2.Password);
                    writer.WriteLine(TextBox3.Text);
                    writer.WriteLine(TextBox4.Text);
                    writer.WriteLine(TextBox5.Text);
                    writer.WriteLine(TextBox6.Text);
                    writer.WriteLine(TextBox7.Text);
                    writer.WriteLine(TextBox8.Text);
                    pop("Confirmed");
                    pass = TextBox2.Password;
                }
            }
            catch (Exception) { pop("Not Confirmed"); }
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
            if (LightDismissAnimatedPopup.IsOpen) { LightDismissAnimatedPopup.IsOpen = false;
            if (errortext.Text.Equals("CONFIRMED!!!"))
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }
            }
            }
        }
        private void pop(String s)
        {
            ShowPopupAnimationClicked("OOPS :( :( :(\n" + s);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Progress.IsActive = true;
            if (check()) return;
            finalise();
            Progress.IsActive = false;
            ShowPopupAnimationClicked("CONFIRMED!!!");
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
        private async void read1()
        {
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
        
    }
}
