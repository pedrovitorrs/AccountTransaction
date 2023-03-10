namespace AccountTransaction.Account.API.Models
{
    public class Conta : Entity
    {
        public int Numero_Conta { get; set; }
        public int Numero_Agencia { get; set; }
        public string? Nome_Titular { get; set; }
        public string? Tipo_Conta { get; set; }
        public string? Identificador_Titular { get; set; }
        public int? Ativa { get; set; }
        public ICollection<Cartao> Cartoes { get; set; }
    }
}
