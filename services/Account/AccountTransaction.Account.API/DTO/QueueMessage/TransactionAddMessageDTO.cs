using AccountTransaction.MessageBus;

namespace AccountTransaction.Account.API.DTO.QueueMessage
{
    public class TransactionAddMessageDTO : BaseMessage
    {
        public string? Numero_Cartao { get; set; }
        public string? Valor_Transacao { get; set; }
    }
}
