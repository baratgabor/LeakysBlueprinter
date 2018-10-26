using Easy.MessageHub;
using LeakysBlueprinter.ViewModels;
using LeakysBlueprinter.Views;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace LeakysBlueprinter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SetCultureForDefaultAndCurrentThread("en-US");

            ShowSplashScreen();

            var messageHub = MessageHub.Instance;

            MainViewModel VM = new MainViewModel(
                messageHub: MessageHub.Instance,
                startupContent: new SetupViewModel(
                    messageHub,
                    () => new MainContentViewModel(messageHub)
                ));

            MainView V = new MainView(VM);

            V.Show();
        }

        public void ShowSplashScreen()
        {
            if (!System.IO.File.Exists("Media/SplashScreen1.jpg"))
                return;

            SplashScreen splash = new SplashScreen("Media/SplashScreen1.jpg");
            splash.Show(true);
            Thread.Sleep(1500);
            splash.Close(new TimeSpan(1000));
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled exception occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            // TODO: Proper exception handling
            e.Handled = true;
        }

        private void SetCultureForDefaultAndCurrentThread(string culture)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(culture);

            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
