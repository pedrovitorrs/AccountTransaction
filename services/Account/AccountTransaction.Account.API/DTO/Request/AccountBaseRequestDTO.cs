using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class AccountBaseRequestDTO
    {
        [Required(ErrorMessage = "O numero da conta é obrigatório")]
        public Nullable<int> Numero_Conta { get; set; }
        [Required(ErrorMessage = "O numero da agencia é obrigatório")]
        public Nullable<int> Numero_Agencia { get; set; }
    }
}
