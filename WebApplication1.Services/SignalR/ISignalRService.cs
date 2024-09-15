using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Services.SignalR
{
    public interface ISignalRService
    {
        void UpdateUserPhotoAndShowActiveConnections(Guid userId, string photoUri);
    }
}
