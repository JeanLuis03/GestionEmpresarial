using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionEmpresarial.Configurations
{
    public class PermisosRolConfiguration : IEntityTypeConfiguration<PermisosRol>
    {
        public void Configure(EntityTypeBuilder<PermisosRol> builder)
        {
            builder.ToTable("PermisosRoles");

            builder.HasKey(x => new
            {
                x.IdRol,
                x.IdPermiso
            });

            builder.HasOne(x => x.Rol)
                   .WithMany(x => x.PermisosRol)
                   .HasForeignKey(x => x.IdRol);

            builder.HasOne(x => x.Permiso)
                   .WithMany(x => x.PermisosRol)
                   .HasForeignKey(x => x.IdPermiso);
        }
    }
}
