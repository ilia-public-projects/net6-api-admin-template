using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.SignalR;

namespace WebApplication1.Services.SignalR
{
    public interface IConnectionManager
    {
        void Add(Guid key, string connectionId, UserSignalRModel user);
        List<UserSignalRModel> GetConnections();
        IEnumerable<string> GetConnections(Guid key);
        void Remove(Guid key, string connectionId);
        void UpdateUserPhoto(Guid userId, string photoUri);
    }
}
