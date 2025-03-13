using Azure.Core;
using BusinessLogic.DTO.ImageProcessingDTO;
using Microsoft.AspNetCore.SignalR;
using ServerAPI.Services;

namespace ServerAPI.Hubs
{
    public class TranferPhotoHub : Hub
    {
        private readonly RabbitMqService _rabbitService;

        public TranferPhotoHub(RabbitMqService rabbitService)
        {
            _rabbitService = rabbitService;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected");
            await base.OnConnectedAsync();
        }


        public async Task SendPhoto(ImageProcessingDTO request)
        {
            string connectionId = Context.ConnectionId;
            await _rabbitService.SendMessageAsync(request.ImageUrl, connectionId, request.PhotoStyle);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
