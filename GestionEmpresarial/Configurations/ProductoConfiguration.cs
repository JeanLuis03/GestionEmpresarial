using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionEmpresarial.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Productos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Marca)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Modelo)
                   .HasMaxLength(100);

            builder.Property(x => x.Precio)
                   .HasPrecision(18, 2);

            builder.Property(x => x.Stock)
                   .IsRequired();

            builder.Property(x => x.CategoriaId)
                   .IsRequired();

            builder.HasIndex(x => x.Codigo)
                   .IsUnique();

            builder.HasOne(x => x.Categoria)
                   .WithMany()
                   .HasForeignKey(x => x.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
