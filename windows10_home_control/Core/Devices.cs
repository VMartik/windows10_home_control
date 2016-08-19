using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization;
using windows10_home_control.Communication;
using windows10_home_control;
using Windows.UI.Core;

namespace windows10_home_control.Core
{
    class Devices
    {
        //public static uint rFidId;
        public static async Task turnOn(byte Pin)
        {
            var Response = await i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode2, Pin, 1);
        }
        public static async Task turnOff(byte Pin)
        {
            var Response = await i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode2, Pin, 0);
        }
        public static async Task <byte[]> StateControl()
        {
            var Response = i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode1).Result;
            return Response;
        }
        public static async Task<byte[]> rfidControl()
        {
            var Response = i2c_protocol.WriteRead(0x40, (byte)Communication.i2c_protocol.Mode.Mode0).Result;
            return Response;
        }
    }
}
