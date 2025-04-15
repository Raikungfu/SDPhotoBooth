using Microsoft.AspNetCore.SignalR;

namespace ServerAPI.Hubs
{
    public class TranferPhotoHub : Hub
    {
        public TranferPhotoHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected");
            await base.OnConnectedAsync();
        }

        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected" + Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
