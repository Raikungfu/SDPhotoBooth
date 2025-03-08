using BusinessLogic.DTO.ImageProcessingDTO;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServerAPI.Services
{
    public class RabbitMqService
    {
        private readonly string _queueName = "image_processing_queue";
        private const string ReplyQueue = "image_response_queue";
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(configuration["RabbitMq:Uri"])
            };
        }

        public async Task SendMessageAsync(string imageUrl, string correlationId, Option option, PersonalInfo personalInfo)
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
                ImageUrl = imageUrl,
                CorrelationId = correlationId,
                option,
                personal_info = personalInfo
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
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: ReplyQueue, durable: false, exclusive: false, autoDelete: false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonConvert.DeserializeObject<dynamic>(message);

                if (data != null)
                {
                    string correlationId = data.CorrelationId;
                    string processedImageUrl = data.ProcessedImageUrl ?? "Processing failed";

                    Console.WriteLine($"Received message with correlation id: {correlationId}, Image: {processedImageUrl}");
                }

                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: ReplyQueue, autoAck: true, consumer: consumer);
        }
    }
}
