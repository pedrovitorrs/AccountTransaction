using AccountTransaction.Transaction.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace AccountTransaction.Transaction.API.Data.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacao")
                .HasKey(u => u.Id);

            builder.Property(u => u.Valor_Transacao).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
