using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> builder)
    {
        //Here you can configure the properties using the object 'Builder'.
        builder.ToTable("mascota");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.NombreMascota).IsRequired().HasMaxLength(50);

        builder.Property(x => x.EspecieMascota).IsRequired().HasMaxLength(50);

        builder.Property(x => x.FechaNacimientoMascota).HasColumnType("date");

        builder.Property(x => x.IdClienteFk).HasColumnType("int");
        builder.HasOne(x => x.Clientes).WithMany(x => x.Mascotas).HasForeignKey(x => x.IdClienteFk);

        builder.Property(x => x.IdRazaFk).HasColumnType("int");
        builder.HasOne(x => x.Razas).WithMany(x => x.Mascotas).HasForeignKey(x => x.IdRazaFk);
    }
}