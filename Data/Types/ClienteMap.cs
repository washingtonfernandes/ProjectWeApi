using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Types
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("cliente");

            builder.Property(i => i.Id)
                .HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
                .HasColumnName("nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80)
                .IsRequired();
                
            builder.Property(i => i.Cpf)
                .HasColumnName("Cpf")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80)
                .IsRequired();  
            
            builder.Property(i => i.DataCadastro)
                .HasColumnName("dataCadastro")
                .HasColumnType("DATE")
                .IsRequired();
        }
    }
}