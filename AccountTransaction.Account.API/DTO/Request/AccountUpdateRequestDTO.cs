using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class AccountUpdateRequestDTO : BaseRequestDTO
    {
        public string? Nome_Titular { get; set; }
        [StringLength(18, ErrorMessage = "O {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 14)]
        public string? Identificador_Titular { get; set; }
        public int? Ativa { get; set; }
    }
}
