using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace AccountTransaction.Account.API.Models
{
    public class Cartao : Entity
    {
        public int Numero_Cartao { get; set; }
        public DateTime Data_Vencimento { get; set; }
        public int CVC { get; set; }
        public int Numero_Conta { get; set; }
        public int Numero_Agencia { get; set; }
        public decimal Limite_Saldo { get; set; }
        public decimal Limite_Saldo_Disponivel { get; set; }
        public int? Ativo { get; set; }
        public int? Bloqueado { get; set; }
        public Conta Conta { get; set; }
    }
}
