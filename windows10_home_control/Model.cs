using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace windows10_home_control
{
    public class DeviceConfigDB : DbContext
    {

    }
    public class Device
    {
        public int DeviceId { get; set;}
        public int DeviceState { get; set; }
    }
}
