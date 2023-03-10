namespace AccountTransaction.Account.API.DTO.Request
{
    public class CardFindAllRequestDTO
    {
        public Nullable<int> Numero_Cartao { get; set; }
        public Nullable<DateTime> Data_Vencimento { get; set; }
        public Nullable<int> CVC { get; set; }
        public Nullable<int> Numero_Conta { get; set; }
        public Nullable<int> Numero_Agencia { get; set; }
        public Nullable<int> Ativo { get; set; }
    }
}
