using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHDT.Infrastructure.Settings
{
    public class MQTTSettings
    {
        public static string Server { get; set; }
        public static string Port { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
    }
}
