using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Transaction.API.DTO.Request
{
    public class TransactionUpdateRequestDTO : TransactionBaseRequestDTO
    {
        [Required]
        public decimal Valor_Transacao { get; set; }
    }
}
