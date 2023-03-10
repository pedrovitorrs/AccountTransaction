namespace AccountTransaction.WebUI.ViewModel.Cartao
{
    public class CartaoDetailsViewModel
    {
        public long Numero_Cartao { get; set; }
        public DateTime Data_Vencimento { get; set; }
        public int CVC { get; set; }
        public int Numero_Conta { get; set; }
        public int Numero_Agencia { get; set; }
        public decimal Limite_Saldo { get; set; }
        public decimal Limite_Saldo_Disponivel { get; set; }
        public int? Ativo { get; set; }
        public int? Bloqueado { get; set; }
    }
}
