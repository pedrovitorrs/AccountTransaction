using AccountTransaction.Account.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class CardAddRequestDTO : CardBaseRequestDTO
    {
        public string? Data_Vencimento { get; set; }
        public int CVC { get; set; }
        public int Numero_Conta { get; set; }
        public int Numero_Agencia { get; set; }
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
