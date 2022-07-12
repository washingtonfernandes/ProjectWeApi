using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produto");

            builder.Property(i => i.Id)
            .HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
            .HasColumnName("nome")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();

            builder.Property(i => i.Preco)
            .HasColumnName("preco")
            .HasColumnType("DECIMAL")
            .IsRequired();

            builder.Property(i => i.QuantVenda)
                .HasColumnName("quantVenda")
                .HasColumnType("INT");

            builder.Property(i => i.Quantidade)
                .HasColumnName("quantidade")
                .HasColumnType("INT");

            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasConstraintName("FK_Produtos_Categoria")
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasMany(i => i.Pedidos)
                .WithMany(i => i.Produtos)
                .UsingEntity<Dictionary<string, object>>(
                    "produto_pedido",
                    pedido => pedido
                        .HasOne<Pedido>()
                        .WithMany()
                        .HasForeignKey("pedido_id")
                        .HasConstraintName("FK_produto_pedido_pedido_id")
                        .OnDelete(DeleteBehavior.Cascade),
                    produto => produto
                        .HasOne<Produto>()
                        .WithMany()
                        .HasForeignKey("produto_id")
                        .HasConstraintName("FK_produto_pedido_produto_id")
                        .OnDelete(DeleteBehavior.Cascade));
        }

    }
}