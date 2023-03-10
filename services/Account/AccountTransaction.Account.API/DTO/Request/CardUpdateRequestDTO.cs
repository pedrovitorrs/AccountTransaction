using AccountTransaction.Commom.Core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class CardUpdateRequestDTO : CardBaseRequestDTO
    {
        [CreditCardExpired(ErrorMessage = "Cartão de crédito expirado")]
        public string? Data_Vencimento { get; set; }
        public Nullable<decimal> Limite_Saldo_Disponivel { get; set; }
        public Nullable<int> Ativo { get; set; }
    }
}
