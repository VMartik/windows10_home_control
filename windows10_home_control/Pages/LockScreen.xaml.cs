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
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Core;
using windows10_home_control.Core;
using windows10_home_control.Communication;


// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace windows10_home_control.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LockScreen : Page
    {
        bool hide_flyout = false;
        CancellationTokenSource cts;
        public LockScreen()
        {
            this.InitializeComponent();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () =>
                    {
                        TimeOnLock.Text = DateTime.Now.ToString("H:mm tt");
                        DateOnLock.Text = DateTime.Now.ToString("dd.MM.yyyy");
                        if(hide_flyout) { waitprogress.Visibility = Visibility.Collapsed; hide_flyout = false; }
                    });
                    await Task.Delay(1000);
                }
            });
        }

       void WaitForCard (CancellationToken ct)
        {
            List<rFidCard> rfids;
            using (var db = new DeviceConfigDB())
            {
                rfids = db.rFidCards.ToList();
                
            }
            Task waitRfid = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if(ct.IsCancellationRequested)
                    {
                        hide_flyout = true;
                        ct.ThrowIfCancellationRequested();
                    }
                    var Response = i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode0).Result;
                    uint rFidId = BitConverter.ToUInt32(Response, 0);
                    if (rfids.Find(item => item.rFidUID.Equals(rFidId))!=null)
                    {
                        MainPage.localSettings.Values["_SignalisationState"] = false;
                        MainPage._isUnlocked = true;
                        cts.Cancel();
                        ct.ThrowIfCancellationRequested();
                    }
                    //if (cts.IsCancellationRequested == true) { collaps.Visibility = Visibility.Collapsed; }
                    /*foreach (rFidCard R in rfids)
                    {
                        if (R.rFidUID == rFidId)
                        {
                            MainPage.localSettings.Values["_SignalisationState"] = false;
                            MainPage._isUnlocked = true;
                            cts.Cancel();
                            unLock.Hide();
                        }
                    }*/
                    /*if (rFidId == 3272815379)
                    {
                        MainPage.localSettings.Values["_SignalisationState"] = false;
                        MainPage._isUnlocked = true;
                        cts.Cancel();
                        unLock.Hide();
                    }*/
                    await Task.Delay(500);
                }
            }, ct);
            cts = null;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //waitprogress.Visibility = Visibility.Collapsed;
        }

        private void UnlockButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //unLock.ShowAt((FrameworkElement)sender);
            waitprogress.Visibility = Visibility.Visible;
            cts = new CancellationTokenSource();
            cts.CancelAfter(10000);
            WaitForCard(cts.Token);
        }
    }
}
