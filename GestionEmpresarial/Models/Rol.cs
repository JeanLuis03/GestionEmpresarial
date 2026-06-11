namespace GestionEmpresarial.Models
{
    public class Rol : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        public ICollection<PermisosRol> PermisosRol { get; set; } = new List<PermisosRol>();

    }
}
