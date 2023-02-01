using AccountTransaction.MessageBus;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Transaction.API.DTO.Request
{
    public class TransactionAddRequestDTO : BaseMessage, IValidatableObject
    {
        [StringLength(16, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 16)]
        [CreditCard(ErrorMessage = "Cartão de crédito inválido")]
        [Required]
        public string Numero_Cartao { get; set; }

        [Required]
        public string Valor_Transacao { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Regex.IsMatch($"{Numero_Cartao}", @"^(51|52|53|54|55)"))
            {
                results.Add(new ValidationResult("Favor informar número de cartão válido para a Bandeira Mastercard.", new string[] { nameof(Numero_Cartao) }));
            }

            _ = decimal.TryParse(Valor_Transacao, out decimal result);
            if (result <= 0)
            {
                results.Add(new ValidationResult("Favor informar um valor de transação válido.", new string[] { nameof(Valor_Transacao) }));
            }
            return results;
        }
    }
}
