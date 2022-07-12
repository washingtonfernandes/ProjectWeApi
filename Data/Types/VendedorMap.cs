using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class VendedorMap : IEntityTypeConfiguration<Vendedor>
    {
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.ToTable("vendedor");
        
        builder.Property(i => i.Id)
            .HasColumnName("id");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Nome)
            .HasColumnName("nome")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(i => i.Bonificacao)
            .HasColumnName("bonificacao")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();

        }
    }
}