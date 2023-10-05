using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        //Here you can configure the properties using the object 'Builder'.
        builder.ToTable("servicio");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreServicio).HasMaxLength(50);

        builder.Property(x => x.PrecioServicio).HasColumnType("double");
    }
}