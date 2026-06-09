using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionEmpresarial.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NombreUsuario)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasIndex(x => x.NombreUsuario)
                   .IsUnique();

            builder.Property(x => x.Correo)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.HasIndex(x => x.Correo)
                   .IsUnique();

            builder.Property(x => x.ContrasenaHash)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(x => x.IntentosFallidos)
                   .HasDefaultValue(0);

            builder.HasOne(x => x.Rol)
                   .WithMany(x => x.Usuarios)
                   .HasForeignKey(x => x.IdRol)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
