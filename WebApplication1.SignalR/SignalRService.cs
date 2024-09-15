using Microsoft.AspNetCore.SignalR;
using System;
using WebApplication1.Services.SignalR;

namespace WebApplication1.SignalR
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<ConnectionHub> hubContext;
        private readonly IConnectionManager connectionManager;

        public SignalRService(IHubContext<ConnectionHub> hubContext, IConnectionManager connectionManager)
        {
            this.hubContext = hubContext;
            this.connectionManager = connectionManager;
        }

        public void UpdateUserPhotoAndShowActiveConnections(Guid userId, string photoUri)
        { 
            connectionManager.UpdateUserPhoto(userId, photoUri);
            hubContext.Clients.All.SendAsync("showActiveUsers", connectionManager.GetConnections());
        }
    }
}
