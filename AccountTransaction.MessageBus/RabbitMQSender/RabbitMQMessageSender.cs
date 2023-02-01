using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountTransaction.MessageBus.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly RabbitMQMessageConfiguration _rabbitSender;

        public RabbitMQMessageSender(RabbitMQMessageConfiguration rabbitSender)
        {
            _rabbitSender = rabbitSender;
        }

        public void SendMessage<T>(BaseMessage message, string queueName)
        {
            if (_rabbitSender.ConnectionExists())
            {
                using var channel = _rabbitSender._connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray<T>(message);
                channel.BasicPublish(
                    exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray<T>(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize((T)Convert.ChangeType(message, typeof(T)), options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
