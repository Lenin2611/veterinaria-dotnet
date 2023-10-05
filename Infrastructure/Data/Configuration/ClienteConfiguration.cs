using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        //Here you can configure the properties using the object 'Builder'.
        builder.ToTable("cliente");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(c => c.NombreCliente).IsRequired().HasMaxLength(50);

        builder.Property(c => c.ApellidoCliente).IsRequired().HasMaxLength(50);

        builder.Property(c => c.EmailCliente).IsRequired().HasMaxLength(50);
    }
}