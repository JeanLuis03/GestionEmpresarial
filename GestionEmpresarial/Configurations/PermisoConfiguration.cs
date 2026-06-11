using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionEmpresarial.Configurations
{
    public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.ToTable("Permisos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Descripcion)
                   .HasMaxLength(250);

        }
    }
}
