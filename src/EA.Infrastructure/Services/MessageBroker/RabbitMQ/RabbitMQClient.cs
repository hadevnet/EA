using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EA.Core.Models.MessageBroker.RabbitMQ;
using EA.Core.MessageBroker.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EA.Infrastructure.Services.MessageBroker.RabbitMQ;
public partial class RabbitMQClient : IRabbitMQClient
{
    private readonly RabbitMQOptions _options;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQClient(RabbitMQOptions options)
    {
        _options = options;
        var factory = new ConnectionFactory
        {
            Uri = new Uri($"amqp://{options.UserName}:{options.Password}@{options.Hostname}:{options.Port}/{options.VirtualHost}")
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendMessage(Message message, string exchangeName = "", string queueName = "", bool persistent = false)
    {
        // Declare a queue
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        // Convert the message to bytes
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = persistent;

        // Publish the message to the queue
        _channel.BasicPublish(exchange: exchangeName, routingKey: queueName, basicProperties: properties, body: body);
    }

    public async Task<Message> ReceiveMessageAsync(string queueName = "")
    {
        // Declare the queue to consume from
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        Message? receivedMessage = new();

        var tcs = new TaskCompletionSource<Message>();

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            receivedMessage = JsonSerializer.Deserialize<Message>(Encoding.UTF8.GetString(body));
            tcs.SetResult(receivedMessage!);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        await tcs.Task; // Wait for the message to be received before returning

        return tcs.Task.Result;
    }
}