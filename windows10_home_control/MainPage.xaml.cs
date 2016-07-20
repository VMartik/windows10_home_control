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

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace windows10_home_control
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


            //Процесс обновле
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () =>
                    {
                        textBlock_Time.Text = DateTime.Now.ToString("H:mm:ss tt");
                        textBlock_Date.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                    });
                    await Task.Delay(1000);
                }
            });

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Core.Devices.StateControl();
                    await Task.Delay(5000);
                }
            });
        }
        private async void button_Click(object sender, RoutedEventArgs e)
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
    }
}
