using AccountTransaction.MessageBus;

namespace AccountTransaction.Transaction.API.DTO.QueueMessage
{
    public class TransactionAddMessageDTO : BaseMessage
    {
        public Guid? Id_Aprovacao { get; set; }
        public string? Numero_Cartao { get; set; }
        public string? Valor_Transacao { get; set; }
    }
}
