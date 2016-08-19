using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace windows10_home_control
{
    public class DeviceConfigDB : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<rFidCard> rFidCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Config.db");
        }
    }
    public class Device
    {
        public int DeviceId { get; set; }
        public int DevicePin { get; set; }
        public int DeviceState { get; set; }
        public int DeviceAlarmState { get; set; }
        public bool DeviceEnable { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
    }
    public class rFidCard
    {
        public int rFidCardId { get; set; }
        public uint rFidUID { get; set; }
        public bool rFidType { get; set; }
    }
}
