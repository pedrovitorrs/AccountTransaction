namespace AccountTransaction.Transaction.API.Models
{
    public class Transacao : Entity
    {
        public long Numero_Cartao { get; set; }
        public Guid Id_Aprovacao { get; set; }
        public DateTime Data_Transacao { get; set; }
        public decimal Valor_Transacao { get; set; }
    }
}
