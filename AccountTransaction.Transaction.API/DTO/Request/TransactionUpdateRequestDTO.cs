using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AccountTransaction.Transaction.API.DTO.Request
{
    public class TransactionUpdateRequestDTO : TransactionBaseRequestDTO
    {
        [Required]
        public decimal Valor_Transacao { get; set; }
        [JsonIgnore]
        public Nullable<Guid> Id_Aprovacao { get; set; }
    }
}
