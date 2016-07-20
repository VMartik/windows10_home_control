using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using windows10_home_control.Core;

namespace windows10_home_control.Communication
{
    public class i2c_protocol
    {
        private static string AQS;
        private static DeviceInformationCollection DIS;

        public enum Mode : byte
        {
            Mode0 = 0,

            Mode1 = 1,
            /// <summary>
            /// Передает IO сигнал на Arduino и получает подтверждение об успешном установлении
            /// </summary>
            Mode2 = 2,
        }

        public static async Task<byte[]> WriteRead(int I2C_Slave_Address, byte ControlMode, byte Pin = 0, byte PinValue = 0)
        {
            byte[] Response = new byte[14];

            try
            {
                var Settings = new I2cConnectionSettings(I2C_Slave_Address);
                Settings.BusSpeed = I2cBusSpeed.StandardMode;

                if (DIS == null || AQS == null)
                {
                    AQS = I2cDevice.GetDeviceSelector("I2C1");
                    DIS = await DeviceInformation.FindAllAsync(AQS);
                }

                using (I2cDevice Device = await I2cDevice.FromIdAsync(DIS[0].Id, Settings))
                {
                    Device.Write(new byte[] { ControlMode, Pin, PinValue });
                    Device.Read(Response);
                }
            }
            catch (Exception)
            {

            }
            return Response;
        }

    }
}
