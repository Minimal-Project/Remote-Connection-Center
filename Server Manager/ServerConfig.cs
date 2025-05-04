using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Manager
{
    public class ServerConfig
    {
        public string Name { get; set; } = "";
        public string Host { get; set; } = "";
        public int Port { get; set; } = 22;
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
