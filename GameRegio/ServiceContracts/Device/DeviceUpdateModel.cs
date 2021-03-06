﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Device
{
    public class DeviceUpdateModel
    {
        public string DeviceId { get; set; }
        public string UserId { get; set; }
        public string DeviceName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Carrier { get; set; }
        public string OperatingSystem { get; set; }
        public double OperatingSystemVersion { get; set; }
        public string Language { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
    }
}
