using AccountTransaction.MessageBus;
using AccountTransaction.MessageBus.RabbitMQSender;
using AccountTransaction.Transaction.API.DTO.QueueMessage;
using AccountTransaction.Transaction.API.DTO.Request;
using AccountTransaction.Transaction.API.Services.Interface;
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

namespace AccountTransaction.Transaction.API.MessageConsumer
{
    public class RabbitMQTransactionProcessedConsumer : BackgroundService
    {
        private readonly RabbitMQMessageConfiguration _rabbitMQMessage;
        private readonly ITransactionService _transactionService;
        private IModel _channel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rabbitMQMessageSender"></param>
        public RabbitMQTransactionProcessedConsumer(
            RabbitMQMessageConfiguration rabbitMQMessage,
            IServiceCollection _services
            )
        {
            _rabbitMQMessage = rabbitMQMessage;
            _transactionService = _services.BuildServiceProvider().GetRequiredService<ITransactionService>();
            if (_rabbitMQMessage.ConnectionExists())
            {
                _channel = _rabbitMQMessage._connection.CreateModel();
                _channel.QueueDeclare(queue: Routing_Keys.TRANSACTION_PROCESSED, false, false, false, arguments: null);
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
            _channel.BasicConsume(Routing_Keys.TRANSACTION_PROCESSED, false, consumer);
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
                var accountCreated = await _transactionService.Create(new TransactionAddRequestDTO() 
                { 
                    Numero_Cartao = transactionAddMessageDTO.Numero_Cartao, 
                    Valor_Transacao = transactionAddMessageDTO.Valor_Transacao 
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
