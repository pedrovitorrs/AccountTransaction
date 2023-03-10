using AccountTransaction.Account.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountTransaction.Account.API.Data.Mappings
{
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.ToTable("Conta")
                .HasKey(u => new { u.Numero_Conta, u.Numero_Agencia });

            builder.Property(u => u.Numero_Conta).IsRequired();
            builder.Property(u => u.Numero_Agencia).IsRequired();
            builder.Property(u => u.Nome_Titular).IsRequired();
            builder.Property(u => u.Tipo_Conta).IsRequired();
            builder.Property(u => u.Identificador_Titular).IsRequired();
            builder.Property(u => u.Ativa);
        }
    }
}
