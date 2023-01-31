using AccountTransaction.Account.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class CardAddRequestDTO : CardBaseRequestDTO
    {
        [Required]
        public string? Data_Vencimento { get; set; }
        [Required]
        public Nullable<int> CVC { get; set; }
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
            return results;
        }
    }
}
