using AccountTransaction.MessageBus;
using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Transaction.API.DTO.Request
{
    public class TransactionBaseRequestDTO : BaseMessage
    {
        public Guid Id { get; set; }
    }
}
