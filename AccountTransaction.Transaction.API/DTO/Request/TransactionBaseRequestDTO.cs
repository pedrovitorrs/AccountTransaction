using AccountTransaction.MessageBus;
using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Transaction.API.DTO.Request
{
    public class TransactionBaseRequestDTO : BaseMessage
    {
        [StringLength(16, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 16)]
        [CreditCard(ErrorMessage = "Cartão de crédito inválido")]
        [Required]
        public string Numero_Cartao { get; set; }
    }
}
