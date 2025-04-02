using BusinessLogic.DTO.ImageProcessingDTO;
using BusinessLogic.DTO.PhotoStyleDTO;
using BusinessLogic.DTO.RabbitMQDTO;
using BussinessObject.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerAPI.Hubs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServerAPI.Services
{
    public class RabbitMqService
    {
        private readonly string ReplyQueue;
        private readonly string SendQueue;
        private readonly ConnectionFactory _factory;
        private readonly IHubContext<TranferPhotoHub> _hubContext;


        public RabbitMqService(IConfiguration configuration, IHubContext<TranferPhotoHub> hubContext)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(configuration["RabbitMq:Uri"])
            };

            _hubContext = hubContext;
            ReplyQueue = configuration["RabbitMq:ReplyQueue"];
            SendQueue = configuration["RabbitMq:SendQueue"];

        }

        public async Task SendMessageAsync(string imageUrl, string correlationId, PhotoStyleResponseDTO photoStyle)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var arguments = new Dictionary<string, object>
                    {
                        { "x-message-ttl", 300000 }
                    };

            await channel.QueueDeclareAsync(queue: SendQueue, durable: false, exclusive: false, autoDelete: false, arguments: arguments);


            var message = new
            {
                imageUrl,
                correlationId,
                photoStyle = photoStyle.Name,
                prompt = photoStyle.Prompt,
                negativePrompt = photoStyle.NegativePrompt,
                controlnets = photoStyle.Controlnets,
                numImagesPerGen = photoStyle.NumImagesPerGen,
                guidanceScale = photoStyle.GuidanceScale,
                strength = photoStyle.Strength,
                faceImage = photoStyle.FaceImage,
                backgroundRemover = photoStyle.BackgroundRemover,
                height = photoStyle.Height,
                width = photoStyle.Width,
                mode = photoStyle.Mode
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var properties = new BasicProperties
            {
                CorrelationId = correlationId,
                ReplyTo = ReplyQueue
            };

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: SendQueue, mandatory: false, basicProperties: properties, body: body);
        }

        public async Task StartListeningAsync()
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: ReplyQueue, durable: false, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonConvert.DeserializeObject<ResponseRabbitMQDTO>(message);

                if (data != null)
                {
                    string correlationId = data.CorrelationId;
                    string[] processedImageUrl = data.ProcessedImageUrl ?? [];

                    Console.WriteLine($"Received message with correlation id: {correlationId}, Image: {processedImageUrl}");

                    await _hubContext.Clients.Client(correlationId).SendAsync("ReceiveProcessedPhoto", processedImageUrl);
                }

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: ReplyQueue, autoAck: true, consumer: consumer);

            await Task.Delay(-1);
        }
    }
}