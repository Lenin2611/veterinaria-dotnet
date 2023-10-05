using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ClienteTelefonoConfiguration : IEntityTypeConfiguration<ClienteTelefono>
{
    public void Configure(EntityTypeBuilder<ClienteTelefono> builder)
    {
        //Here you can configure the properties using the object 'Builder'.
        builder.ToTable("clientetelefono");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(x => x.NumeroTelefono).HasColumnType("int");

        builder.HasOne(c => c.Clientes).WithMany(c => c.ClienteTelefonos).HasForeignKey(c => c.IdClienteFk);
    }
}