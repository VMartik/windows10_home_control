using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using windows10_home_control.Core;
using windows10_home_control.Communication;
using System.Threading.Tasks;
using System.Threading;
using Windows.System;
using Windows.Storage;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace windows10_home_control
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        public static bool _isLocked = true;
        public static bool _isUnlocked;
        CancellationTokenSource _CollectData_cancell;
        public MainPage()
        {
            this.InitializeComponent();
            MainFrame.Navigate(typeof(Pages.Home));
            //Процесс обновлегия UI
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () =>
                    {
                        TimeLine.Text = DateTime.Now.ToString("H:mm:ss tt");
                        DateLine.Text = DateTime.Now.ToString("dd.MM.yyyy");
                        if (Convert.ToBoolean(localSettings.Values["_SignalisationState"]) == true && _isLocked == true)
                        {
                            MainFrame.Navigate(typeof(Pages.LockScreen));
                            MainPanel.Visibility = Visibility.Collapsed;
                            TimeDate.Visibility = Visibility.Collapsed;
                            _isLocked = false;
                        }
                        else if (Convert.ToBoolean(localSettings.Values["_SignalisationState"]) == false && _isUnlocked == true)
                        {
                            MainFrame.Navigate(typeof(Pages.Home));
                            MainPanel.Visibility = Visibility.Visible;
                            TimeDate.Visibility = Visibility.Visible;
                            _isUnlocked = false;
                        }
                    });
                    await Task.Delay(1000);
                }
            });
            /*Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Core.Devices.StateControl();
                    await Task.Delay(5000);
                }
            });*/
        }
        void Collect(CancellationToken ct)
        {
            Task CollectData = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    ct.ThrowIfCancellationRequested();
                    await Core.Devices.StateControl();
                    await Task.Delay(5000);
                }
            }, ct);
            //CollectData.Start();
            //CollectData.Wait();
            _CollectData_cancell = null;
        }


        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageSplitView.IsPaneOpen = !MainPageSplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Hamburger_Home.IsSelected)
            {
                MainFrame.Navigate(typeof(Pages.Home));
            }
            else if(Hamburger_Music.IsSelected)
            {
                MainFrame.Navigate(typeof(Pages.Music));
            }
            else if (Hamburger_Weather.IsSelected)
            {

            }
            else if(Hamburger_Security.IsSelected)
            {
                MainFrame.Navigate(typeof(Pages.Security));
            }
            else if(Hamburger_Settings.IsSelected)
            {
                MainFrame.Navigate(typeof(Pages.Settings));
            }

        }

        private void PiRestart_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShutdownManager.BeginShutdown(ShutdownKind.Restart, TimeSpan.Zero);
            Shutdown_FlyOut.Hide();
            MainPageSplitView.IsPaneOpen = false;
        }

        private void PiShutdown_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, TimeSpan.Zero);
            Shutdown_FlyOut.Hide();
            MainPageSplitView.IsPaneOpen = false;
        }

        private void PowerButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Shutdown_FlyOut.ShowAt((FrameworkElement)sender);
            PowerButton.IsSelected = false;
        }

        private void AlarmOn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            localSettings.Values["_SignalisationState"] = true;
            _isLocked = true;
            AlarmOn.IsSelected = false;
            Shutdown_FlyOut.Hide();
            MainPageSplitView.IsPaneOpen = false;
            //cts = new CancellationTokenSource();
            //cts.Cancel();
            //Collect(cts.Token);
        }

        private void SecurityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MusicList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /*private async void button_Click(object sender, RoutedEventArgs e)
{
var Response = await i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode2, 13, 1);

}
private async void button1_Click_1(object sender, RoutedEventArgs e)
{
var Response = await i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode2, 13, 0);
}

private async void toggleSwitch_Toggled(object sender, RoutedEventArgs e)
{
if (toggleSwitch.IsOn == true) { await Core.Devices.turnOn(23); }
else { await Core.Devices.turnOff(23); }
}

private async void toggleSwitch1_Toggled(object sender, RoutedEventArgs e)
{
if (toggleSwitch1.IsOn == true) { await Core.Devices.turnOn(25); }
else { await Core.Devices.turnOff(25); }
}

private async void button_Click_1(object sender, RoutedEventArgs e)
{
using (var db = new DeviceConfigDB())
{
var dev = new Device { DevicePin = 31, DeviceName = "Relay1", DeviceEnable = true, DeviceAlarmState = 0, DeviceState = 1, DeviceType = "Relay" };
db.Devices.Add(dev);
db.SaveChanges();
}
}

private void button1_Click(object sender, RoutedEventArgs e)
{
using (var db = new DeviceConfigDB())
{
textBlock.Text = db.Devices.Count().ToString();
}
}*/
    }
}
