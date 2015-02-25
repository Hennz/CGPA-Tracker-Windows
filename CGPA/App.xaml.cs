using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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
using Windows.UI.Popups;
using Windows.UI;
using Microsoft.Live;
using System.Threading.Tasks;

namespace CGPA
{
    sealed partial class App : Application
    {
        Rect _windowBounds;
        double _settingsWidth = 346;
        Popup _settingsPopup;
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        void AppCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand cmd = new SettingsCommand("account", "Account", (x) =>
            {
                _settingsPopup = new Popup();
                _settingsPopup.Closed += OnPopupClosed;
                Window.Current.Activated += OnWindowActivated;
                _settingsPopup.IsLightDismissEnabled = true;
                _settingsPopup.Width = _settingsWidth;
                _settingsPopup.Height = _windowBounds.Height;

                AccFly mypane = new AccFly();
                mypane.Width = _settingsWidth;
                mypane.Height = _windowBounds.Height;

                _settingsPopup.Child = mypane;
                _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);
                _settingsPopup.IsOpen = true;
            });
            args.Request.ApplicationCommands.Add(cmd);
            var about = new SettingsCommand("about", "About the App", OpenAbout);
            args.Request.ApplicationCommands.Add(about);
            var features = new SettingsCommand("features", "Features of the App", OpenFeatures);
            args.Request.ApplicationCommands.Add(features);
            // Privacy settings command.
            cmd = new SettingsCommand("privacy", "Privacy", (x) =>
            {
                _settingsPopup = new Popup();
                _settingsPopup.Closed += OnPopupClosed;
                Window.Current.Activated += OnWindowActivated;
                _settingsPopup.IsLightDismissEnabled = true;
                _settingsPopup.Width = _settingsWidth;
                _settingsPopup.Height = _windowBounds.Height;

                fly mypane = new fly();
                mypane.Width = _settingsWidth;
                mypane.Height = _windowBounds.Height;

                _settingsPopup.Child = mypane;
                _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);
                _settingsPopup.IsOpen = true;
            });
            args.Request.ApplicationCommands.Add(cmd);
        }
        void OnWindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            _windowBounds = Window.Current.Bounds;
        }
        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                _settingsPopup.IsOpen = false;
            }
        }
        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }
        private async void OpenFeatures(IUICommand command)
        {
            Uri uri = new Uri("http://srujanjha.wix.com/cgpa-tracker#!features-of-the-app/c1v1z");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void OpenAbout(IUICommand command)
        {
            Uri uri = new Uri("http://srujanjha.wix.com/cgpa-tracker#!functionality-of-the-app/c1xzx");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void OpenPrivacyPolicy(IUICommand command)
        {
            Uri uri = new Uri("http://srujanjha.wix.com/cgpa-tracker#!privacy-policy/cdjk");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
            _windowBounds = Window.Current.Bounds;
            Window.Current.SizeChanged += OnWindowSizeChanged;
            SettingsPane.GetForCurrentView().CommandsRequested += AppCommandsRequested;
        }
        public static async Task updateUserName(TextBlock userName, Boolean signIn)
        {
            try
            {
                LiveAuthClient LCAuth = new LiveAuthClient();
                LiveLoginResult LCLoginResult = await LCAuth.InitializeAsync();
                try
                {
                    LiveLoginResult loginResult = null;
                    if (signIn)
                    {
                        loginResult = await LCAuth.LoginAsync(new string[] { "wl.basic" });
                    }
                    else
                    {
                        loginResult = LCLoginResult;
                    }
                    if (loginResult.Status == LiveConnectSessionStatus.Connected)
                    {
                        LiveConnectClient connect = new LiveConnectClient(LCAuth.Session);
                        LiveOperationResult operationResult = await connect.GetAsync("me");
                        dynamic result = operationResult.Result;
                        if (result != null)
                        {
                            userName.Text = string.Join(" ", "Hello", result.name, "!");
                        }
                        else
                        {
                            userName.Text = "You're signed in.";
                        }
                    }
                    else
                    {
                        userName.Text = "You're not signed in.";
                    }
                }
                catch (LiveAuthException)
                {
                    // Handle the exception. 
                }
            }
            catch (LiveAuthException)
            {
                // Handle the exception. 
            }
            catch (LiveConnectException)
            {
                // Handle the exception. 
            }
        }
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        
    }
}
