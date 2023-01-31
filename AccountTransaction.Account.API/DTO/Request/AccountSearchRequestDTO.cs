using AccountTransaction.Account.API.Models;
using Newtonsoft.Json;

namespace AccountTransaction.Account.API.DTO.Request
{
    public class AccountSearchRequestDTO
    {
        public Nullable<int> Numero_Conta { get; set; }
        public Nullable<int> Numero_Agencia { get; set; }
        public string? Nome_Titular { get; set; }
        public string? Tipo_Conta { get; set; }
        public string? Identificador_Titular { get; set; }
        public Nullable<int> Ativa { get; set; }
    }
}
