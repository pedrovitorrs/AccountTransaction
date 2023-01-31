using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Account.API.DTO.Request
{
    public abstract class CardBaseRequestDTO
    {
        [StringLength(16, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 16)]
        [CreditCard(ErrorMessage = "Cartão de crédito inválido")]
        public Nullable<int> Numero_Cartao { get; set; }
    }
}
