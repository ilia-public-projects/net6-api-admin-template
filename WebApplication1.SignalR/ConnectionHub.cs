using Microsoft.AspNetCore.SignalR;
using WebApplication1.Models.SignalR;
using WebApplication1.Services.SignalR;

namespace WebApplication1.SignalR
{
    public class ConnectionHub : Hub
    {
        private readonly IConnectionManager connectionManager;

        public ConnectionHub(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public void ShowActiveConnections()
        {
            Clients.Client(Context.ConnectionId).SendAsync("showActiveUsers", connectionManager.GetConnections());
        }

        public override Task OnConnectedAsync()
        {
            var user = BuildUser();

            if (user != null)
            {
                connectionManager.Add(user.Id, Context.ConnectionId, user);
                Clients.All.SendAsync("showActiveUsers", connectionManager.GetConnections());
            }

            var result = base.OnConnectedAsync();
            return result;
        }

        private UserSignalRModel BuildUser()
        {
            string name = Context.GetHttpContext().Request.Query["name"].ToString() ?? string.Empty;
            string userId = Context.GetHttpContext().Request.Query["userId"].ToString() ?? string.Empty;
            string email = Context.GetHttpContext().Request.Query["email"].ToString() ?? string.Empty;
            string photoUri = Context.GetHttpContext().Request.Query["photoUri"].ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            Guid userGuid;
            Guid.TryParse(userId, out userGuid);

            var result = new UserSignalRModel()
            {
                Name = name,
                Id = userGuid,
                Email = email,
                PhotoUri = photoUri,
            };
            return result;
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
            var user = BuildUser();
            if (user != null)
            {
                connectionManager.Remove(user.Id, Context.ConnectionId);

                Clients.All.SendAsync("showActiveUsers", connectionManager.GetConnections());
            }

            return base.OnDisconnectedAsync(ex);
        }

    }
}
