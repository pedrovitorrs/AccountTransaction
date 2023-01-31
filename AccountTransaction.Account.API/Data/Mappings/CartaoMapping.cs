using AccountTransaction.Account.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace AccountTransaction.Account.API.Data.Mappings
{
    public class CartaoMapping : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.ToTable("Cartao")
                .HasKey(u => u.Numero_Cartao);

            builder.Property(u => u.Data_Vencimento).IsRequired();
            builder.Property(u => u.CVC).IsRequired();
            builder.Property(u => u.Numero_Conta).IsRequired();
            builder.Property(u => u.Numero_Agencia).IsRequired();
            builder.Property(u => u.Limite_Saldo).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(u => u.Limite_Saldo_Disponivel).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(u => u.Ativo);
            builder.Property(u => u.Bloqueado);
            builder.HasOne(u => u.Conta)
                .WithMany(u => u.Cartoes)
                .HasForeignKey(d => new { d.Numero_Conta, d.Numero_Agencia });
        }
    }
}
