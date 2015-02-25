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
using Facebook;
using Windows.Networking.Connectivity;
using Windows.Networking.BackgroundTransfer;

namespace CGPA
{
    public sealed partial class sgpa : CGPA.Common.LayoutAwarePage
    {
        Boolean newu = true;
        float cg = 0.0f;
        float gp = 0.0f;
        int tgp = 0;
        public sgpa()
        {
            this.InitializeComponent();
            close0();
            read1(); read();
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
                    if (text == null) { newu = false; errortext.TextAlignment = TextAlignment.Center; if (welcome.Text.Equals("Hello")) { newu = true; poping("You are a new user.\nPlease update your profile to keep track of your SGPA.\nEither Sign In using your Live Account\n or create a temporary profile in Profile Section..."); } }
                    else
                    { newu = false; }
                }
            }
            catch (Exception) { }
        }
        private async void poping(String s)
        {
            var messageDialog = new MessageDialog(s);
            messageDialog.Commands.Add(new UICommand("Go to Profile Section", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand("Go to Main Page", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.CancelCommandIndex = 1;
            await messageDialog.ShowAsync();
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label.Equals("Go to Profile Section"))
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(profile));
                }
            }
            else
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }
            }

        }
        private void open0()
        {
            TextBox00.Visibility = Visibility.Visible;
            TextBox01.Visibility = Visibility.Visible;
            ComboBox0.Visibility = Visibility.Visible;
        }
        private void close0()
        {
            Ad1.Visibility = Visibility.Visible;
            TextBlock3.Visibility = Visibility.Collapsed;
            TextBlock4.Visibility = Visibility.Collapsed;
            TextBox00.Visibility = Visibility.Collapsed;
            TextBox01.Visibility = Visibility.Collapsed;
            ComboBox0.Visibility = Visibility.Collapsed;
            close1(); close2(); close3(); close4(); close5(); close6(); close7(); close8(); close9(); close10(); close11();
            TextBox00.Text = "";
            TextBox10.Text = "";
            TextBox20.Text = "";
            TextBox30.Text = "";
            TextBox40.Text = "";
            TextBox50.Text = "";
            TextBox60.Text = "";
            TextBox70.Text = "";
            TextBox80.Text = "";
            TextBox90.Text = "";
            TextBox10.Text = "";
            TextBox110.Text = "";
            TextBox01.Text = "";
            TextBox11.Text = "";
            TextBox21.Text = "";
            TextBox31.Text = "";
            TextBox41.Text = "";
            TextBox51.Text = "";
            TextBox61.Text = "";
            TextBox71.Text = "";
            TextBox81.Text = "";
            TextBox91.Text = "";
            TextBox101.Text = "";
            TextBox111.Text = "";
            ComboBox0.SelectedIndex = -1;
            ComboBox1.SelectedIndex = -1;
            ComboBox2.SelectedIndex = -1;
            ComboBox3.SelectedIndex = -1;
            ComboBox4.SelectedIndex = -1;
            ComboBox5.SelectedIndex = -1;
            ComboBox6.SelectedIndex = -1;
            ComboBox7.SelectedIndex = -1;
            ComboBox8.SelectedIndex = -1;
            ComboBox9.SelectedIndex = -1;
            ComboBox10.SelectedIndex = -1;
            ComboBox11.SelectedIndex = -1;
        }
        private void open1()
        {
            TextBox10.Visibility = Visibility.Visible;
            TextBox11.Visibility = Visibility.Visible;
            ComboBox1.Visibility = Visibility.Visible;

        }
        private void close1()
        {
            TextBox10.Visibility = Visibility.Collapsed;
            TextBox11.Visibility = Visibility.Collapsed;
            ComboBox1.Visibility = Visibility.Collapsed;
        }
        private void open2()
        {
            TextBox20.Visibility = Visibility.Visible;
            TextBox21.Visibility = Visibility.Visible;
            ComboBox2.Visibility = Visibility.Visible;

        }
        private void close2()
        {
            TextBox20.Visibility = Visibility.Collapsed;
            TextBox21.Visibility = Visibility.Collapsed;
            ComboBox2.Visibility = Visibility.Collapsed;
        }
        private void open3()
        {
            TextBox30.Visibility = Visibility.Visible;
            TextBox31.Visibility = Visibility.Visible;
            ComboBox3.Visibility = Visibility.Visible;
        }
        private void close3()
        {
            TextBox30.Visibility = Visibility.Collapsed;
            TextBox31.Visibility = Visibility.Collapsed;
            ComboBox3.Visibility = Visibility.Collapsed;
        }
        private void open4()
        {
            TextBox40.Visibility = Visibility.Visible;
            TextBox41.Visibility = Visibility.Visible;
            ComboBox4.Visibility = Visibility.Visible;
        }
        private void close4()
        {
            TextBox40.Visibility = Visibility.Collapsed;
            TextBox41.Visibility = Visibility.Collapsed;
            ComboBox4.Visibility = Visibility.Collapsed;
        }
        private void open5()
        {
            TextBox50.Visibility = Visibility.Visible;
            TextBox51.Visibility = Visibility.Visible;
            ComboBox5.Visibility = Visibility.Visible;
        }
        private void close5()
        {
            TextBox50.Visibility = Visibility.Collapsed;
            TextBox51.Visibility = Visibility.Collapsed;
            ComboBox5.Visibility = Visibility.Collapsed;
        }
        private void open6()
        {
            TextBox60.Visibility = Visibility.Visible;
            TextBox61.Visibility = Visibility.Visible;
            ComboBox6.Visibility = Visibility.Visible;
        }
        private void close6()
        {
            TextBox60.Visibility = Visibility.Collapsed;
            TextBox61.Visibility = Visibility.Collapsed;
            ComboBox6.Visibility = Visibility.Collapsed;
        }
        private void open7()
        {
            TextBox70.Visibility = Visibility.Visible;
            TextBox71.Visibility = Visibility.Visible;
            ComboBox7.Visibility = Visibility.Visible;
        }
        private void close7()
        {
            TextBox70.Visibility = Visibility.Collapsed;
            TextBox71.Visibility = Visibility.Collapsed;
            ComboBox7.Visibility = Visibility.Collapsed;
        }
        private void open8()
        {
            TextBox80.Visibility = Visibility.Visible;
            TextBox81.Visibility = Visibility.Visible;
            ComboBox8.Visibility = Visibility.Visible;
        }
        private void close8()
        {
            TextBox80.Visibility = Visibility.Collapsed;
            TextBox81.Visibility = Visibility.Collapsed;
            ComboBox8.Visibility = Visibility.Collapsed;
        }
        private void open9()
        {
            TextBox90.Visibility = Visibility.Visible;
            TextBox91.Visibility = Visibility.Visible;
            ComboBox9.Visibility = Visibility.Visible;
        }
        private void close9()
        {
            TextBox90.Visibility = Visibility.Collapsed;
            TextBox91.Visibility = Visibility.Collapsed;
            ComboBox9.Visibility = Visibility.Collapsed;
        }
        private void open10()
        {
            TextBox100.Visibility = Visibility.Visible;
            TextBox101.Visibility = Visibility.Visible;
            ComboBox10.Visibility = Visibility.Visible;
        }
        private void close10()
        {
            TextBox100.Visibility = Visibility.Collapsed;
            TextBox101.Visibility = Visibility.Collapsed;
            ComboBox10.Visibility = Visibility.Collapsed;
        }
        private void open11()
        {
            TextBox110.Visibility = Visibility.Visible;
            TextBox111.Visibility = Visibility.Visible;
            ComboBox11.Visibility = Visibility.Visible;
        }
        private void close11()
        {
            TextBox110.Visibility = Visibility.Collapsed;
            TextBox111.Visibility = Visibility.Collapsed;
            ComboBox11.Visibility = Visibility.Collapsed;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ch = (int)Scenarios.SelectedIndex;
            if (ch > 5)
            {
                Ad1.Visibility = Visibility.Collapsed;
                TextBlock4.Visibility = Visibility.Visible;
                TextBlock3.Visibility = Visibility.Visible;
            }
            else
            {
                Ad1.Visibility = Visibility.Visible;
                TextBlock3.Visibility = Visibility.Visible;
                TextBlock4.Visibility = Visibility.Collapsed;
            }
            switch (ch)
            {
                case 11:
                    {
                        open11(); open10(); open9(); open8(); open7(); open6(); open5(); open4(); open3(); open2(); open1(); open0();
                        break;
                    }
                case 10:
                    {
                        open10(); open9(); open8(); open7(); open6(); open5(); open4(); open3(); open2(); open1(); open0(); close11();
                        break;
                    }
                case 9:
                    {
                        open9(); open8(); open7(); open6(); open5(); open4(); open3(); open2(); open1(); open0(); close10(); close11();
                        break;
                    }
                case 8:
                    {
                        open8(); open7(); open6(); open5(); open4(); open3(); open2(); open1(); open0(); close9(); close10(); close11();
                        break;
                    }
                case 7:
                    {
                        open7(); open6(); open5(); open4(); open3(); open2(); open1(); open0(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 6:
                    {
                        open6(); open5(); open4(); open3(); open2(); open1(); open0(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 5:
                    {
                        open5(); open4(); open3(); open2(); open1(); open0(); close6(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 4:
                    {
                        open4(); open3(); open2(); open1(); open0(); close5(); close6(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 3:
                    {
                        open3(); open2(); open1(); open0(); close4(); close5(); close6(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 2:
                    {
                        open2(); open1(); open0(); close3(); close4(); close5(); close6(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 1:
                    {
                        open1(); open0(); close2(); close3(); close4(); close5(); close6(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
                case 0:
                    {
                        open0(); close1(); close2(); close3(); close4(); close5(); close6(); close7(); close8(); close9(); close10(); close11();
                        break;
                    }
            }

        }
        private int credits(int s)
        {
            switch (s)
            {
                case 0: return 10;
                case 1: return 9;
                case 2: return 8;
                case 3: return 7;
                case 4: return 6;
                case 5: return 5;
                case 6: return 4;
                case 7: return 2;
                default: return 0;
            }
        }
        private int tcredits()
        {
            int ch = (int)Scenarios.SelectedIndex;
            switch (ch)
            {
                case 11:
                    {
                        tgp = (Convert.ToInt32(TextBox111.Text) +
                            Convert.ToInt32(TextBox101.Text) +
                            Convert.ToInt32(TextBox91.Text) +
                            Convert.ToInt32(TextBox81.Text) +
                            Convert.ToInt32(TextBox71.Text) +
                            Convert.ToInt32(TextBox61.Text) +
                            Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 10:
                    {
                        tgp = (Convert.ToInt32(TextBox101.Text) +
                            Convert.ToInt32(TextBox91.Text) +
                            Convert.ToInt32(TextBox81.Text) +
                            Convert.ToInt32(TextBox71.Text) +
                            Convert.ToInt32(TextBox61.Text) +
                            Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 9:
                    {
                        tgp = (Convert.ToInt32(TextBox91.Text) +
                            Convert.ToInt32(TextBox81.Text) +
                            Convert.ToInt32(TextBox71.Text) +
                            Convert.ToInt32(TextBox61.Text) +
                            Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 8:
                    {
                        tgp = (Convert.ToInt32(TextBox81.Text) +
                            Convert.ToInt32(TextBox71.Text) +
                            Convert.ToInt32(TextBox61.Text) +
                            Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 7:
                    {
                        tgp = (Convert.ToInt32(TextBox71.Text) +
                            Convert.ToInt32(TextBox61.Text) +
                            Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 6:
                    {
                        tgp = (Convert.ToInt32(TextBox61.Text) +
                            Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 5:
                    {
                        tgp = (Convert.ToInt32(TextBox51.Text) +
                            Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 4:
                    {
                        tgp = (Convert.ToInt32(TextBox41.Text) +
                            Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 3:
                    {
                        tgp = (Convert.ToInt32(TextBox31.Text) +
                            Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 2:
                    {
                        tgp = (Convert.ToInt32(TextBox21.Text) +
                            Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 1:
                    {
                        tgp = (Convert.ToInt32(TextBox11.Text) +
                            Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
                case 0:
                    {
                        tgp = (Convert.ToInt32(TextBox01.Text)); return tgp;
                    }
            }
            return 1;
        }
        private Boolean check()
        {
            if (newu) { pop("You are a new user.\n Please update your profile to keep track of your CGPA\nGo to Profile Section..."); return true; }
            int ch;
            ch = (int)Semester.SelectedIndex;
            if (ch == -1) { pop("Probably, You didn't select the Semester"); return true; }
            ch = (int)Scenarios.SelectedIndex;
            if (ch == -1) { pop("Probably, You didn't select the number of Courses."); ch = -1; return true; }
            try
            {
                int m = Int32.Parse(TextBox01.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 1st course isn't a number."); return true;
            }
            if (TextBox00.Text.Equals("")) { pop("Probably, you haven't given the 1st course's name."); return true; }
            if (ComboBox0.SelectedIndex == -1) { pop("Probably, you haven't given the 1st Course's Grade."); return true; }
            if (ch < 1) return false;
            try
            {
                int m = Int32.Parse(TextBox11.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 2nd course isn't a number."); return true;
            }
            if (TextBox10.Text.Equals("")) { pop("Probably, you haven't given the 2nd course's name."); return true; }
            if (ComboBox1.SelectedIndex == -1) { pop("Probably, you haven't given the 2nd Course's Grade."); return true; }
            if (ch < 2) return false;
            try
            {
                int m = Int32.Parse(TextBox21.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 3rd course isn't a number."); return true;
            } if (TextBox20.Text.Equals("")) { pop("Probably, you haven't given the 3rd course's name."); return true; }
            if (ComboBox2.SelectedIndex == -1) { pop("Probably, you haven't given the 3rd Course's Grade."); return true; }
            if (ch < 3) return false;
            try
            {
                int m = Int32.Parse(TextBox31.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 4th course isn't a number."); return true;
            } if (TextBox30.Text.Equals("")) { pop("Probably, you haven't given the 4th course's name."); return true; }
            if (ComboBox3.SelectedIndex == -1) { pop("Probably, you haven't given the 4th Course's Grade."); return true; }
            if (ch < 4) return false;
            try
            {
                int m = Int32.Parse(TextBox41.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 5th course isn't a number."); return true;
            } if (TextBox40.Text.Equals("")) { pop("Probably, you haven't given the 5th course's name."); return true; }
            if (ComboBox4.SelectedIndex == -1) { pop("Probably, you haven't given the 5th Course's Grade."); return true; }
            if (ch < 5) return false;
            try
            {
                int m = Int32.Parse(TextBox51.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 6th course isn't a number."); return true;
            } if (TextBox50.Text.Equals("")) { pop("Probably, you haven't given the 6th course's name."); return true; }
            if (ComboBox5.SelectedIndex == -1) { pop("Probably, you haven't given the 6th Course's Grade."); return true; }
            if (ch < 6) return false;
            try
            {
                int m = Int32.Parse(TextBox61.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 7th course isn't a number."); return true;
            } if (TextBox60.Text.Equals("")) { pop("Probably, you haven't given the 7th course's name."); return true; }
            if (ComboBox6.SelectedIndex == -1) { pop("Probably, you haven't given the 7th Course's Grade."); return true; }
            if (ch < 7) return false;
            try
            {
                int m = Int32.Parse(TextBox71.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 8th course isn't a number."); return true;
            } if (TextBox70.Text.Equals("")) { pop("Probably, you haven't given the 8th course's name."); return true; }
            if (ComboBox7.SelectedIndex == -1) { pop("Probably, you haven't given the 8th Course's Grade."); return true; }
            if (ch < 8) return false;
            try
            {
                int m = Int32.Parse(TextBox81.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 9th course isn't a number."); return true;
            } if (TextBox80.Text.Equals("")) { pop("Probably, you haven't given the 9th course's name."); return true; }
            if (ComboBox8.SelectedIndex == -1) { pop("Probably, you haven't given the 9th Course's Grade."); return true; }
            if (ch < 9) return false;
            try
            {
                int m = Int32.Parse(TextBox91.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 10th course isn't a number."); return true;
            } if (TextBox90.Text.Equals("")) { pop("Probably, you haven't given the 10th course's name."); return true; }
            if (ComboBox9.SelectedIndex == -1) { pop("Probably, you haven't given the 10th Course's Grade."); return true; }
            if (ch < 10) return false;
            try
            {
                int m = Int32.Parse(TextBox101.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 11th course isn't a number."); return true;
            } if (TextBox100.Text.Equals("")) { pop("Probably, you haven't given the 11th course's name."); return true; }
            if (ComboBox10.SelectedIndex == -1) { pop("Probably, you haven't given the 11th Course's Grade."); return true; }
            if (ch < 11) return false;
            try
            {
                int m = Int32.Parse(TextBox111.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 12th course isn't a number."); return true;
            }
            if (TextBox110.Text.Equals("")) { pop(ch + "Probably, you haven't given the 12th course's name."); return true; }
            if (ComboBox11.SelectedIndex == -1) { pop("Probably, you haven't given the 12th Course's Grade."); return true; }
            return false;
        }
        private async void finalise(float cg)
        {
            int n = -1;
            int no = -1;
            String text;
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("sgpa" + ((int)Semester.SelectedIndex).ToString() + ".txt", CreationCollisionOption.OpenIfExists);
                Stream writeStream = await storageFile.OpenStreamForWriteAsync();
                using (StreamWriter writer = new StreamWriter(writeStream))
                {
                    no = Scenarios.SelectedIndex;
                    writer.WriteLine(cg.ToString());
                    ShowPopupAnimationClicked("\nYou have secured " + cg * tgp + " grade points out of total " + tgp * 10 + " Grade Points.\n Hence, Your SGPA for semester " + (Semester.SelectedIndex + 1) + " is " + cg.ToString("0.00"));
                    writer.WriteLine(tcredits().ToString());
                    text = Convert.ToString(no);
                    writer.WriteLine(text);
                    writer.WriteLine(TextBox00.Text);
                    writer.WriteLine(TextBox01.Text); ;
                    n = ComboBox0.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 1) return;
                    writer.WriteLine(TextBox10.Text);
                    writer.WriteLine(TextBox11.Text);
                    n = ComboBox1.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 2) return;
                    writer.WriteLine(TextBox20.Text);
                    writer.WriteLine(TextBox21.Text);
                    n = ComboBox2.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 3) return;
                    writer.WriteLine(TextBox30.Text);
                    writer.WriteLine(TextBox31.Text);
                    n = ComboBox3.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 4) return;
                    writer.WriteLine(TextBox40.Text);
                    writer.WriteLine(TextBox41.Text);
                    n = ComboBox4.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 5) return;
                    writer.WriteLine(TextBox50.Text);
                    writer.WriteLine(TextBox51.Text);
                    n = ComboBox5.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 6) return;
                    writer.WriteLine(TextBox60.Text);
                    writer.WriteLine(TextBox61.Text);
                    n = ComboBox6.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 7) return;
                    writer.WriteLine(TextBox70.Text);
                    writer.WriteLine(TextBox71.Text);
                    n = ComboBox7.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 8) return;
                    writer.WriteLine(TextBox80.Text);
                    writer.WriteLine(TextBox81.Text);
                    n = ComboBox8.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 9) return;
                    writer.WriteLine(TextBox90.Text);
                    writer.WriteLine(TextBox91.Text);
                    n = ComboBox9.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 10) return;
                    writer.WriteLine(TextBox100.Text);
                    writer.WriteLine(TextBox101.Text);
                    n = ComboBox10.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                    if (no < 11) return;
                    writer.WriteLine(TextBox110.Text);
                    writer.WriteLine(TextBox111.Text);
                    n = ComboBox11.SelectedIndex;
                    text = Convert.ToString(n);
                    writer.WriteLine(text);
                }
            }
            catch (Exception)
            {

            }
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (check()) return;
            int ch = (int)Scenarios.SelectedIndex;
            switch (ch)
            {
                case 11:
                    {
                        cg = cg + (Convert.ToInt32(TextBox111.Text) * credits(ComboBox11.SelectedIndex) +
                            Convert.ToInt32(TextBox101.Text) * credits(ComboBox10.SelectedIndex) +
                            Convert.ToInt32(TextBox91.Text) * credits(ComboBox9.SelectedIndex) +
                            Convert.ToInt32(TextBox81.Text) * credits(ComboBox8.SelectedIndex) +
                            Convert.ToInt32(TextBox71.Text) * credits(ComboBox7.SelectedIndex) +
                            Convert.ToInt32(TextBox61.Text) * credits(ComboBox6.SelectedIndex) +
                            Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 10:
                    {
                        cg = cg + (Convert.ToInt32(TextBox101.Text) * credits(ComboBox10.SelectedIndex) +
                            Convert.ToInt32(TextBox91.Text) * credits(ComboBox9.SelectedIndex) +
                            Convert.ToInt32(TextBox81.Text) * credits(ComboBox8.SelectedIndex) +
                            Convert.ToInt32(TextBox71.Text) * credits(ComboBox7.SelectedIndex) +
                            Convert.ToInt32(TextBox61.Text) * credits(ComboBox6.SelectedIndex) +
                            Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 9:
                    {
                        cg = cg + (Convert.ToInt32(TextBox91.Text) * credits(ComboBox9.SelectedIndex) +
                            Convert.ToInt32(TextBox81.Text) * credits(ComboBox8.SelectedIndex) +
                            Convert.ToInt32(TextBox71.Text) * credits(ComboBox7.SelectedIndex) +
                            Convert.ToInt32(TextBox61.Text) * credits(ComboBox6.SelectedIndex) +
                            Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 8:
                    {
                        cg = cg + (Convert.ToInt32(TextBox81.Text) * credits(ComboBox8.SelectedIndex) +
                            Convert.ToInt32(TextBox71.Text) * credits(ComboBox7.SelectedIndex) +
                            Convert.ToInt32(TextBox61.Text) * credits(ComboBox6.SelectedIndex) +
                            Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 7:
                    {
                        cg = cg + (Convert.ToInt32(TextBox71.Text) * credits(ComboBox7.SelectedIndex) +
                            Convert.ToInt32(TextBox61.Text) * credits(ComboBox6.SelectedIndex) +
                            Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 6:
                    {
                        cg = cg + (Convert.ToInt32(TextBox61.Text) * credits(ComboBox6.SelectedIndex) +
                            Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 5:
                    {
                        cg = cg + (Convert.ToInt32(TextBox51.Text) * credits(ComboBox5.SelectedIndex) +
                            Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 4:
                    {
                        cg = cg + (Convert.ToInt32(TextBox41.Text) * credits(ComboBox4.SelectedIndex) +
                            Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 3:
                    {
                        cg = cg + (Convert.ToInt32(TextBox31.Text) * credits(ComboBox3.SelectedIndex) +
                            Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 2:
                    {
                        cg = cg + (Convert.ToInt32(TextBox21.Text) * credits(ComboBox2.SelectedIndex) +
                            Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 1:
                    {
                        cg = cg + (Convert.ToInt32(TextBox11.Text) * credits(ComboBox1.SelectedIndex) +
                            Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
                case 0:
                    {
                        cg = cg + (Convert.ToInt32(TextBox01.Text) * credits(ComboBox0.SelectedIndex));
                        break;
                    }
            }
            cg = cg / tcredits();
            //int cgp = (int)(cg * 100.0);
            //cg = cgp / 100;
            TextBlock1.Visibility = Visibility.Visible;
            TextBlock1.Text = "Your SGPA is " + cg.ToString("0.00");
            finalise(cg); gp = cg; ShowShare();
            cg = 0.0f;

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
        private void TextBox01_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox01.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 1st course isn't a number.");
            }
        }
        private void TextBox11_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox11.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 2nd course isn't a number.");
            }
        }
        private void TextBox21_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox21.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 3rd course isn't a number.");
            }
        }
        private void TextBox31_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox31.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 4th course isn't a number.");
            }
        }
        private void TextBox41_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox41.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 5th course isn't a number.");
            }
        }
        private void TextBox51_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox51.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 6th course isn't a number.");
            }
        }
        private void TextBox61_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox01.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 7th course isn't a number.");
            }
        }
        private void TextBox71_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox71.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 8th course isn't a number.");
            }
        }
        private void TextBox81_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox81.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 9th course isn't a number.");
            }
        }
        private void TextBox91_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox91.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 10th course isn't a number.");
            }
        }
        private void TextBox101_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox101.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 11th course isn't a number.");
            }
        }
        private void TextBox111_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int m = Int32.Parse(TextBox111.Text);
            }
            catch (Exception)
            {
                pop("Probably, the number of credits given by you for 12th course isn't a number.");
            }
        }
        private async void initialize()
        {
            int n = -1;
            int no = -1;
            float cg;
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync("sgpa" + ((int)Semester.SelectedIndex).ToString() + ".txt", CreationCollisionOption.OpenIfExists);
                Stream readStream = await storageFile.OpenStreamForReadAsync();
                using (StreamReader reader = new StreamReader(readStream))
                {
                    String text = reader.ReadLine();
                    if (text == null) { Scenarios.SelectedIndex = -1; close0(); return; }
                    cg = Convert.ToSingle(text);
                    TextBlock1.Visibility = Visibility.Visible;
                    TextBlock1.Text = "Your SGPA is\n" + text;
                    text = reader.ReadLine();
                    text = reader.ReadLine();
                    no = Convert.ToInt32(text);
                    Scenarios.SelectedIndex = no;
                    text = reader.ReadLine();
                    TextBox00.Text = text;
                    text = reader.ReadLine();
                    TextBox01.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox0.SelectedIndex = n;
                    if (no < 1) return;
                    text = reader.ReadLine();
                    TextBox10.Text = text;
                    text = reader.ReadLine();
                    TextBox11.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox1.SelectedIndex = n;
                    if (no < 2) return;
                    text = reader.ReadLine();
                    TextBox20.Text = text;
                    text = reader.ReadLine();
                    TextBox21.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox2.SelectedIndex = n;
                    if (no < 3) return;
                    text = reader.ReadLine();
                    TextBox30.Text = text;
                    text = reader.ReadLine();
                    TextBox31.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox3.SelectedIndex = n;
                    if (no < 4) return;
                    text = reader.ReadLine();
                    TextBox40.Text = text;
                    text = reader.ReadLine();
                    TextBox41.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox4.SelectedIndex = n;
                    if (no < 5) return;
                    text = reader.ReadLine();
                    TextBox50.Text = text;
                    text = reader.ReadLine();
                    TextBox51.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox5.SelectedIndex = n;
                    if (no < 6) return;
                    text = reader.ReadLine();
                    TextBox60.Text = text;
                    text = reader.ReadLine();
                    TextBox61.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox6.SelectedIndex = n;
                    if (no < 7) return;
                    text = reader.ReadLine();
                    TextBox70.Text = text;
                    text = reader.ReadLine();
                    TextBox71.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox7.SelectedIndex = n;
                    if (no < 8) return;
                    text = reader.ReadLine();
                    TextBox80.Text = text;
                    text = reader.ReadLine();
                    TextBox81.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox8.SelectedIndex = n;
                    if (no < 9) return;
                    text = reader.ReadLine();
                    TextBox90.Text = text;
                    text = reader.ReadLine();
                    TextBox91.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox9.SelectedIndex = n;
                    if (no < 10) return;
                    text = reader.ReadLine();
                    TextBox100.Text = text;
                    text = reader.ReadLine();
                    TextBox101.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox10.SelectedIndex = n;
                    if (no < 11) return;
                    text = reader.ReadLine();
                    TextBox110.Text = text;
                    text = reader.ReadLine();
                    TextBox111.Text = text;
                    text = reader.ReadLine();
                    n = Convert.ToInt32(text);
                    ComboBox11.SelectedIndex = n;
                }
            }
            catch (Exception)
            {
                Scenarios.SelectedIndex = no;
            }
        }
        private void Semester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            close0(); initialize();
            TextBlock1.Visibility = Visibility.Collapsed;
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
        private async void PublishStory()
        {
            try
            {
                if (Ftext.Text.Equals("Want to add something...")) Ftext.Text = "";
                await this.loginButton.RequestNewPermissions("publish_stream");
                var facebookClient = new Facebook.FacebookClient(this.loginButton.CurrentSession.AccessToken);
                var postParams = new
                {
                    name = "CGPA-Tracker",
                    caption = "Find Your CGPA with CGPA-Tracker.",
                    description = "My SGPA for " + (Semester.SelectedIndex + 1) + " Semester is " + gp.ToString() + ". " + Ftext.Text,
                    link = "http://srujanjha.wix.com/cgpa-tracker",
                    picture = "http://static.wixstatic.com/media/927532_c5688829d7fabd40b42b8fbe515ddbba.png_srz_90_90_75_22_0.50_1.20_0.00_png_srz"
                };
                dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/feed", postParams);
                var result = (IDictionary<string, object>)fbPostTaskResult;

                CloseShare();
            }
            catch (Exception)
            {
                ferror.Visibility = Visibility.Visible;
                if (!IsInternet()) { CloseShare(); pop("Internet Connectivity is Lost."); }
            }
        }
        private void ShowShare()
        {
            if (!IsInternet()) { CloseShare(); pop("You need to have Interent Connection to SHARE."); return; }
            if (!share.IsOpen)
            {
                ftext.Text = "My SGPA for " + (Semester.SelectedIndex + 1) + " Semester is " + cg.ToString() + ".";
                share.IsOpen = true;
                rectangle.Visibility = Visibility.Visible;
                rectangle2.Visibility = Visibility.Visible;
            }
        }
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        private void TextBlock_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 44, 66, 106);
            rectangle.Fill = mySolidColorBrush;
        }
        private void TextBlock_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 56, 85, 136);
            rectangle.Fill = mySolidColorBrush;
        }
        private void TextBlock_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 78, 118, 191);
            rectangle.Fill = mySolidColorBrush;
        }
        private void TextBlock_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 44, 66, 106);
            rectangle.Fill = mySolidColorBrush;
        }
        private void TextBlock1_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 44, 66, 106);
            rectangle2.Fill = mySolidColorBrush;
        }
        private void TextBlock1_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 56, 85, 136);
            rectangle2.Fill = mySolidColorBrush;
        }
        private void TextBlock1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 78, 118, 191);
            rectangle2.Fill = mySolidColorBrush;
        }
        private void TextBlock1_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Windows.UI.Color.FromArgb(255, 44, 66, 106);
            rectangle2.Fill = mySolidColorBrush;
        }
        private void OnShareButtonClick(object sender, TappedRoutedEventArgs e)
        {
            this.PublishStory();
        }
        private void CloseShare()
        {
            if (share.IsOpen)
            {
                share.IsOpen = false; rectangle.Visibility = Visibility.Collapsed;
                rectangle2.Visibility = Visibility.Collapsed;
            }
        }
        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e) { CloseShare(); }

    }
}