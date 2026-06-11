namespace GestionEmpresarial.Models
{
    public class Permiso : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public ICollection<PermisosRol> PermisosRol { get; set; } = new List<PermisosRol>();
    }
}
