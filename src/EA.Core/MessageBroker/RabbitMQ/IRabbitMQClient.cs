using System;
using System.Threading.Tasks;
using EA.Core.Models.MessageBroker.RabbitMQ;

namespace EA.Core.MessageBroker.RabbitMQ;
public interface IRabbitMQClient
{
    void SendMessage(Message message, string exchangeName = "", string queueName = "", bool persistent = false);
    Task<Message> ReceiveMessageAsync(string queueName = "");
}