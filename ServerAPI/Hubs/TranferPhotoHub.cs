using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ServerAPI.Hubs
{
    public class TranferPhotoHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _sessionConnections = new();
        public TranferPhotoHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected");
            await base.OnConnectedAsync();
        }

        public async Task<object> GetConnectionId(string sessionCode)
        {
            var added = _sessionConnections.TryAdd(sessionCode, Context.ConnectionId);
            if (!added)
            {
                return new
                {
                    success = false,
                    message = $"Session code {sessionCode} is already in use by another device.",
                    currentConnectionId = _sessionConnections[sessionCode]
                };
            }

            return new { success = true, connectionId = Context.ConnectionId, _sessionConnections };
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var sessionToRemove = _sessionConnections.FirstOrDefault(kv => kv.Value == Context.ConnectionId).Key;

            if (sessionToRemove != null)
            {
                _sessionConnections.Remove(sessionToRemove, out _);
            }

            Console.WriteLine("Client disconnected" + Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
