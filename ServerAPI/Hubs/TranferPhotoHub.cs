using Microsoft.AspNetCore.SignalR;
using ServerAPI.Services;
using BusinessLogic.DTO.ImageProcessingDTO;

namespace ServerAPI.Hubs
{
    public class TranferPhotoHub : Hub
    {
        private readonly FirebaseService _firebaseService;
        private readonly RabbitMqService _rabbitService;

        public TranferPhotoHub(FirebaseService firebaseService, RabbitMqService rabbitService)
        {
            _firebaseService = firebaseService;
            _rabbitService = rabbitService;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Client connected");
            await base.OnConnectedAsync();
        }
        public async Task SendPhoto(string image, string photoStyle)
        {
            string connectionId = Context.ConnectionId;

            byte[] imageBytes = Convert.FromBase64String(image); 

            string firebaseUrl = await _firebaseService.UploadImageAsync(imageBytes);
            Console.WriteLine($"Image uploaded to Firebase: {firebaseUrl}");

            await _rabbitService.SendMessageAsync(firebaseUrl, connectionId, photoStyle);
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected" + Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
