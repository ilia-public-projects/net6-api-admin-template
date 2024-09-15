using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.SignalR;

namespace WebApplication1.SignalR
{
    public class SignalRConnection
    {
        public UserSignalRModel User { get; set; }
        public HashSet<string> Connections { get; set; }

        public SignalRConnection()
        {
            Connections = new HashSet<string>();
            User = new UserSignalRModel();
        }
    }
}
