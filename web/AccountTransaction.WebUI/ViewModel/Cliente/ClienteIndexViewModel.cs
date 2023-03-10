using AccountTransaction.WebUI.ViewModel.Cartao;

namespace AccountTransaction.WebUI.ViewModel.Cliente
{
    public class ClienteIndexViewModel
    {
        public int Numero_Conta { get; set; }
        public int Numero_Agencia { get; set; }
        public string? Nome_Titular { get; set; }
        public string? Tipo_Conta { get; set; }
        public string? Identificador_Titular { get; set; }
        public int? Ativa { get; set; }
        public ICollection<CartaoDetailsViewModel> Cartoes { get; set; }
    }
}
