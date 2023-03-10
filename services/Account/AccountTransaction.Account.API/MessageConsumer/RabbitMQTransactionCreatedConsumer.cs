using AccountTransaction.Account.API.DTO.QueueMessage;
using AccountTransaction.Account.API.DTO.Request;
using AccountTransaction.Account.API.Services.Interface;
using AccountTransaction.Account.API.Tipos;
using AccountTransaction.MessageBus;
using AccountTransaction.MessageBus.RabbitMQSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace AccountTransaction.Account.API.MessageConsumer
{
    public class RabbitMQTransactionCreatedConsumer : BackgroundService
    {
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private readonly RabbitMQMessageConfiguration _rabbitMQMessage;
        private readonly ICardService _cardService;
        private IModel _channel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rabbitMQMessageSender"></param>
        public RabbitMQTransactionCreatedConsumer(
            IRabbitMQMessageSender rabbitMQMessageSender, 
            RabbitMQMessageConfiguration rabbitMQMessage,
            IServiceCollection _services
            )
        {
            _rabbitMQMessageSender = rabbitMQMessageSender;
            _rabbitMQMessage = rabbitMQMessage;
            _cardService = _services.BuildServiceProvider().GetRequiredService<ICardService>();
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
                var card = await _cardService.FindByNumeroCartao(long.Parse(transactionAddMessageDTO.Numero_Cartao));
                if (card == null || card.Ativo == (int)TipoSituacaoAtividade.INATIVA || !decimal.TryParse(transactionAddMessageDTO?.Valor_Transacao, out decimal valorTransacao) || card?.Limite_Saldo_Disponivel < valorTransacao || card?.Limite_Saldo_Disponivel - valorTransacao < 0)
                {
                    return;
                }

                var accountUpdated = await _cardService.Update(new CardUpdateRequestDTO() { Numero_Cartao = transactionAddMessageDTO.Numero_Cartao, Limite_Saldo_Disponivel = card?.Limite_Saldo_Disponivel - valorTransacao });
                if (accountUpdated != null)
                    _rabbitMQMessageSender.SendMessage<TransactionAddMessageDTO>(transactionAddMessageDTO, Routing_Keys.TRANSACTION_PROCESSED);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
