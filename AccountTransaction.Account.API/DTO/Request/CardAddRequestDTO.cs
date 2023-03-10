using AccountTransaction.Account.API.Models;
using AccountTransaction.Commom.Core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class CardAddRequestDTO : CardBaseRequestDTO, IValidatableObject
    {
        [Required]
        [CreditCardExpired(ErrorMessage = "Cartão de crédito expirado")]
        public string? Data_Vencimento { get; set; }
        [Required]
        [MaxLength(length: 3)]
        public string CVC { get; set; }
        [Required]
        public Nullable<int> Numero_Conta { get; set; }
        [Required]
        public Nullable<int> Numero_Agencia { get; set; }
        [Required]
        public decimal Limite_Saldo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Regex.IsMatch($"{Numero_Cartao}", @"^(51|52|53|54|55)"))
            {
                results.Add(new ValidationResult("Favor informar número de cartão válido para a Bandeira Mastercard", new string[] { nameof(Numero_Cartao) }));
            }

            if (!Data_Vencimento.Contains('/') || Data_Vencimento.Count(letra => '/'.Equals(letra)) > 1)
            {
                results.Add(new ValidationResult("Data Vencimento deve ser informado no formato mm/yy", new string[] { nameof(Data_Vencimento) }));
            }
            return results;
        }
    }
}
