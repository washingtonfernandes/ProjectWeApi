using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("pedido");

            builder.Property(i => i.Id)
            .HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.DataPedido)
                .HasColumnName("dataPedido")
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(i => i.ValorTotal)
                .HasColumnName("valorTotal")
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Pedidos)
                .HasConstraintName("FK_Pedidos_Cliente")
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.Vendedor)
                .WithMany(x => x.Pedidos)
                .HasConstraintName("FK_Pedidos_Vendedor")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Pagamento)
                .WithMany(x => x.Pedidos)
                .HasConstraintName("FK_Pedidos_Pagamento")
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}