namespace GestionEmpresarial.Models
{
    public class PermisosRol
    {
        public Guid IdRol { get; set; }

        public Guid IdPermiso { get; set; }

        public Rol Rol { get; set; } = null!;

        public Permiso Permiso { get; set; } = null!;
    }
}
