namespace AccountTransaction.WebUI.ViewModel.Home
{
    public class IndexViewModel
    {
        public int Total_Transacoes { get; set; }
        public decimal Ticket_Medio { get; set; }
        public decimal Volume_Transacionado { get; set; }
        public long? Cartoes_Cadastrados {get;set;}
    }
}
