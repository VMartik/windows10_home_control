using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace windows10_home_control
{
    public class DeviceConfigDB : DbContext
    {
        public DbSet<Device> Devices { get; set; }
    }
    public class Device
    {
        public int DeviceId { get; set; }
        public int DeviceState { get; set; }
        public int DeviceAlarmState { get; set; }
        public bool DeviceEnable { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
    }
}
