using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models.SignalR;
using WebApplication1.Services.SignalR;

namespace WebApplication1.SignalR
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly Dictionary<Guid, SignalRConnection> connections = new();

        public List<UserSignalRModel> GetConnections()
        {
            return connections.Select(x => x.Value.User).OrderBy(x => x.Name).ToList();
        }

        public void Add(Guid key, string connectionId, UserSignalRModel user)
        {
            lock (connections)
            {
                if (!connections.TryGetValue(key, out SignalRConnection userConnection))
                {
                    userConnection = new();
                    userConnection.User = user;
                    connections.Add(key, userConnection);
                }

                lock (userConnection)
                {
                    userConnection.User = user;
                    userConnection.Connections.Add(connectionId);
                }
            }
        }

        public void UpdateUserPhoto(Guid userId, string photoUri)
        {
            foreach (KeyValuePair<Guid, SignalRConnection> connection in connections)
            {
                if (connection.Value.User.Id == userId)
                {
                    connection.Value.User.PhotoUri = photoUri;
                }
            }
        }

        public IEnumerable<string> GetConnections(Guid key)
        {
            if (connections.TryGetValue(key, out SignalRConnection userConnection))
            {
                return userConnection.Connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(Guid key, string connectionId)
        {
            lock (connections)
            {
                if (!connections.TryGetValue(key, out SignalRConnection userConnection))
                {
                    return;
                }

                lock (userConnection)
                {
                    userConnection.Connections.Remove(connectionId);

                    if (userConnection.Connections.Count == 0)
                    {
                        connections.Remove(key);
                    }
                }
            }
        }
    }
}
