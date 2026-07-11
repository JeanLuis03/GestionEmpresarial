using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionEmpresarial.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Apellido)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Telefono)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.Correo)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Direccion)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.HasIndex(x => x.Correo)
                   .IsUnique();

            builder.HasIndex(x => x.Apellido);

            builder.HasIndex(x => x.Nombre);
        }
    }
}
