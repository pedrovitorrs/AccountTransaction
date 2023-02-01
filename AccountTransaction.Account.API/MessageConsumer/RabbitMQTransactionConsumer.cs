using AccountTransaction.Account.API.DTO.QueueMessage;
using AccountTransaction.MessageBus;
using AccountTransaction.MessageBus.RabbitMQSender;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AccountTransaction.Account.API.MessageConsumer
{
    public class RabbitMQTransactionConsumer : BackgroundService
    {
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly RabbitMQMessageConfiguration _rabbitMQMessage;
        private IModel _channel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rabbitMQMessageSender"></param>
        public RabbitMQTransactionConsumer(IRabbitMQMessageSender rabbitMQMessageSender, RabbitMQMessageConfiguration rabbitMQMessage)
        {
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _rabbitMQMessage = rabbitMQMessage;
            if (_rabbitMQMessage.ConnectionExists())
            {
                _channel = _rabbitMQMessage._connection.CreateModel();
                _channel.QueueDeclare(queue: Routing_Keys.TRANSACTION_CREATED, false, false, false, arguments: null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                TransactionAddMessageDTO vo = JsonSerializer.Deserialize<TransactionAddMessageDTO>(content);
                ProcessTransaction(vo).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(Routing_Keys.TRANSACTION_CREATED, false, consumer);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transactionAddMessageDTO"></param>
        /// <returns></returns>
        private async Task ProcessTransaction(TransactionAddMessageDTO transactionAddMessageDTO)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
    }
}
