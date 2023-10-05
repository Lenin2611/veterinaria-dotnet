using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ClienteDireccionConfiguration : IEntityTypeConfiguration<ClienteDireccion>
{
    public void Configure(EntityTypeBuilder<ClienteDireccion> builder)
    {
        //Here you can configure the properties using the object 'Builder'.
        builder.ToTable("clientedireccion");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(c => c.TipoVia).IsRequired().HasMaxLength(50);

        builder.Property(c => c.NumeroPrincipal).HasColumnType("int");

        builder.Property(c => c.LetraPrincipal).IsRequired().HasMaxLength(50);
    
        builder.Property(c => c.Bis).IsRequired().HasMaxLength(50);
    
        builder.Property(c => c.LetraSecundaria).IsRequired().HasMaxLength(50);

        builder.Property(c => c.CardinalPrimario).IsRequired().HasMaxLength(50);

        builder.Property(c => c.NumeroSecundario).HasColumnType("int");

        builder.Property(c => c.LetraTerciaria).IsRequired().HasMaxLength(50);

        builder.Property(c => c.NumeroTerciario).HasColumnType("int");

        builder.Property(c => c.CardinalSecundario).IsRequired().HasMaxLength(50);

        builder.Property(c => c.Complemento).IsRequired().HasMaxLength(50);

        builder.Property(c => c.CodigoPostal).IsRequired().HasMaxLength(50);

        builder.HasOne(c => c.Ciudades).WithMany(c => c.ClienteDirecciones).HasForeignKey(c => c.IdCiudadFk);
        builder.HasOne(c => c.Clientes).WithOne(c => c.ClienteDirecciones).HasForeignKey<ClienteDireccion>(c => c.IdClienteFk);
    }
}