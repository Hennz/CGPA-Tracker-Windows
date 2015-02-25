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
    public sealed partial class profile : CGPA.Common.LayoutAwarePage
    {
        Boolean newu = true;
        String pass = "";
        public profile()
        {
            this.InitializeComponent();
            read1();
            read();
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
        private void button()
        {
            if (newu)
            {
                Button2.Visibility = Visibility.Collapsed;
                Button1.Visibility = Visibility.Visible;
                Button3.Visibility = Visibility.Collapsed;
            }
            else
            {
                Button2.Visibility = Visibility.Visible;
                Button1.Visibility = Visibility.Collapsed;
                Button3.Visibility = Visibility.Visible;
            }
            track1();
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
                    if (text == null) { newu = false; if (welcome.Text.Equals("Hello")) { newu = true; button(); } }
                    else
                    {
                        pass = reader.ReadLine();
                        newu = false; button();
                    }
                }
            }
            catch (Exception) { }
            if (newu) return;
            float cg=0.0f;
            int credits=0;
            float tgp = 0.0f;
            int tcredits=0;
            Boolean newq=false;
            for (int i = 0; i < 12; i++)
            {
                try
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile storageFile = await localFolder.CreateFileAsync("sgpa" + i + ".txt", CreationCollisionOption.OpenIfExists);
                    Stream readStream = await storageFile.OpenStreamForReadAsync();
                    using (StreamReader reader = new StreamReader(readStream))
                    {
                        String text = reader.ReadLine();
                        if (text == null) { continue; }
                        else
                        {
                            newq=true;
                            cg = (Convert.ToSingle(text));
                            text = reader.ReadLine();
                            credits = (Convert.ToInt32(text));
                            tgp = tgp + (cg*credits);
                            tcredits += credits;
                        }
                    }
                }
                catch (Exception) { }
            }
            if (newq)
            {
                TextBlock1.Visibility = Visibility.Visible;
                TextBlock1.Text = "Sum of total grade points are " + tgp + "\nSum of total credits is " + tcredits * 10 + "\nYour current CGPA is\n" + (tgp / tcredits).ToString("0.00");
            }
            
        }
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(personald));
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(personald));
            }
        }
        private async void track1()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder; 
                StorageFile storageFile = await localFolder.CreateFileAsync("History.txt", CreationCollisionOption.OpenIfExists); 
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String text = reader.ReadLine(); 
                    int no = Convert.ToInt32(text);
                    if (no == 1) {Track.IsOn = true;}
                    else {Track.IsOn = false;}
                }
            }
            catch (Exception) {  }
        }
        private async void track2()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("History.txt", CreationCollisionOption.OpenIfExists);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    if (Track.IsOn) { writer.WriteLine("1");  }
                    else { writer.WriteLine("0");  }
                }
            }
            catch (Exception) { }
        }
        private void Track_Toggled(object sender, RoutedEventArgs e)
        {
            Progress.IsActive = true; track2(); Progress.IsActive = false;
        }
        private async void del()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("Important.txt", CreationCollisionOption.ReplaceExisting);
                await storageFile.DeleteAsync();
            }
            catch (Exception) {  }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("History.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("1");
                }
            }
            catch (Exception) {  }
            for (int i = 0; i < 12; i++)
            {
                try
                {
                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile storageFile = await localFolder.CreateFileAsync("sgpa"+i+".txt", CreationCollisionOption.ReplaceExisting);
                    await storageFile.DeleteAsync();
                }
                catch (Exception) { }
            }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("main.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("gpa.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("personal.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("personald.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("sgpa.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("about.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
       
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("profile.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("scale.txt", CreationCollisionOption.ReplaceExisting);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    writer.WriteLine("0");
                }
            }
            catch (Exception) { }
            
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("Important.txt", CreationCollisionOption.ReplaceExisting);
                await storageFile.DeleteAsync();
            }
            catch (Exception) { }
            this.InitializeComponent();
            read();
            
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Progress.IsActive = true;
            TextBlock1.Visibility = Visibility.Collapsed;
            ShowPopupAnimationClicked("Give your Password to authenticate the deletion");
           // poping("Are you sure you want to delete Everything...\nThis include Profile Info,Themes, GPA history,SGPA ...");
            if (!newu)
            {
                TextBlock.Visibility = Visibility.Visible;
                Password.Visibility = Visibility.Visible;
                confirm.Visibility = Visibility.Visible;
            }
            else
            {
                poping("Are you sure you want to delete Everything...\nThis include Profile Info,Themes, GPA history,SGPA ...");
            }
            Progress.IsActive = false;
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
        private async void poping(String s)
        {
            var messageDialog = new MessageDialog(s);
            messageDialog.Commands.Add(new UICommand("Yes",new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand("No",new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.DefaultCommandIndex = 1;
            messageDialog.CancelCommandIndex = 0;
            await messageDialog.ShowAsync();
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            if(command.Label.Equals("Yes"))
            {del();ShowPopupAnimationClicked("Files DELETED");}
        }
        private void pop(String s)
        {
            ShowPopupAnimationClicked("OOPS :( :( :(\n" + s);
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
            catch (Exception e) { Theme.SelectedIndex = 0; AdsText.Text = e.ToString(); }
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
        private void confirm_click(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Equals(pass))
            {
                poping("Are you sure you want to delete Everything...\nThis include Profile Info,Themes, GPA history,SGPA ...");
                TextBlock.Visibility = Visibility.Collapsed;
                Password.Visibility = Visibility.Collapsed;
                confirm.Visibility = Visibility.Collapsed;
            }
            else pop("Passwords Do Not Match...");
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Ad1.Visibility = Visibility.Collapsed;
            Ad2.Visibility = Visibility.Collapsed;
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Ad1.Refresh();
            Ad2.Refresh();
            Ad1.Visibility = Visibility.Visible;
            Ad2.Visibility = Visibility.Visible;

        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Know));
            }
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
