using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Manager
{
    public class ServerConfig
    {
        public string Name { get; set; } = "none";
        public string Host { get; set; } = "0.0.0.0";
        public int Port { get; set; } = 22;
        public string User { get; set; } = "";
        public string Mode { get; set; } = "SSH";
    }
}
