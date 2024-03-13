using NUnit.Framework;
using System;
using System.Threading.Tasks;
using EA.Core.MessageBroker.RabbitMQ;
using EA.Infrastructure.Services.MessageBroker.RabbitMQ;
using EA.Core.Models.MessageBroker.RabbitMQ;
using RabbitMQ.Client;

namespace EA.Infrastructure.Tests.Services.MessageBroker.RabbitMQ;

public class RabbitMQClientTests
{
    private IRabbitMQClient? _rabbitMQClient;

    [SetUp]
    public void Setup()
    {
        // Replace with your own configuration values
        var options = new RabbitMQOptions
        {
            //// Azure
            Hostname = "38.242.226.210",
            Port = 32035,
            VirtualHost = "",
            //VirtualHost = "finance", // If you want to add virtualHost you need to add only the name 'finance'
            UserName = "guest",
            Password = "guest"

            //// Local Host
            //Hostname = "localhost",
            //Port = 5672,
            //VirtualHost = "",
            //VirtualHost = "finance", // If you want to add virtualHost you need to add only the name 'finance'
            //UserName = "guest",
            //Password = "guest"
        };

        _rabbitMQClient = new RabbitMQClient(options);
    }

    [Test]
    public void SendMessageAsync_WithValidMessage_ShouldSucceed()
    {
        // Arrange
        var message = new Message
        {
            Title = "Test Message",
            Body = "This is a test message.",
            Timestamp = DateTime.UtcNow
        };

        // Act
        _rabbitMQClient?.SendMessage(message);

        // Assert
        // No exceptions should have been thrown
    }

    [Test]
    public async Task ReceiveMessageAsync_WithValidMessage_ShouldReturnMessage()
    {
        // Arrange
        var message = new Message
        {
            Title = "Test Message",
            Body = "This is a test message.",
            Timestamp = DateTime.UtcNow
        };

        string queueId = Guid.NewGuid().ToString();

        _rabbitMQClient?.SendMessage(message, queueName: queueId);

        // Act
        var receivedMessage = await _rabbitMQClient!.ReceiveMessageAsync(queueName: queueId);

        // Assert
        Assert.That(receivedMessage, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(receivedMessage.Title, Is.EqualTo(message.Title));
            Assert.That(receivedMessage.Body, Is.EqualTo(message.Body));
            Assert.That(receivedMessage.Timestamp, Is.EqualTo(message.Timestamp));
        });
    }
}