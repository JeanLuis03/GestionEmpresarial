using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionEmpresarial.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Descripcion)
                   .HasMaxLength(250);

            builder.Property(x => x.Activo)
                   .IsRequired();
        }
    }
}
