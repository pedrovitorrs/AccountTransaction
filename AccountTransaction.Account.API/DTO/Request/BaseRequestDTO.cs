using System.ComponentModel.DataAnnotations;

namespace AccountTransaction.Account.API.DTO.Request
{
    public abstract class BaseRequestDTO
    {
        [Required(ErrorMessage = "O numero da conta é obrigatório")]
        public int Numero_Conta { get; set; }
        [Required(ErrorMessage = "O numero da agencia é obrigatório")]
        public int Numero_Agencia { get; set; }
    }
}
