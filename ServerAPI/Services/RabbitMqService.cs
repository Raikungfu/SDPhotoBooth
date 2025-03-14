using BusinessLogic.DTO.ImageProcessingDTO;
using BusinessLogic.DTO.RabbitMQDTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerAPI.Hubs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServerAPI.Services
{


    public class RabbitMqService : IHostedService
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly IHubContext<TranferPhotoHub> _hubContext;
        private readonly string _queueName = "image_processing_queue";
        private const string ReplyQueue = "image_response_queue";

        public RabbitMqService(IConfiguration configuration, IHubContext<TranferPhotoHub> hubContext)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(configuration["RabbitMq:Uri"])
            };

            _hubContext = hubContext;
        }

        public async Task SendMessageAsync(string imageUrl, string correlationId, string photoStyle)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            var arguments = new Dictionary<string, object>
                    {
                        { "x-message-ttl", 300000 }
                    };

            await channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);

            var message = new
            {
                imageUrl,
                correlationId,
                photoStyle
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            var properties = new BasicProperties
            {
                CorrelationId = correlationId,
                ReplyTo = ReplyQueue
            };

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _queueName, mandatory: false, basicProperties: properties, body: body);
        }

        public async Task StartListeningAsync()
        {
            _connection = await _factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: _queueName, durable: false, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(_channel);
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

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            };

            await _channel.BasicConsumeAsync(queue: ReplyQueue, autoAck: false, consumer: consumer);

            await Task.Delay(Timeout.Infinite);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _channel?.CloseAsync()!;
            await _connection?.CloseAsync()!;
        }

        public Task StartAsync(CancellationToken cancellationToken) => StartListeningAsync();
    }

}
