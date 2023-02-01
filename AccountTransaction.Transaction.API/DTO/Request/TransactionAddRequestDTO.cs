using AccountTransaction.MessageBus;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Transaction.API.DTO.Request
{
    public class TransactionAddRequestDTO : TransactionBaseRequestDTO, IValidatableObject
    {
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
