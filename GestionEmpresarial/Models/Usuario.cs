namespace GestionEmpresarial.Models
{
    public class Usuario : BaseEntity
    {
        public string NombreUsuario { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string ContrasenaHash { get; set; } = string.Empty;

        public Guid IdRol { get; set; }

        public int IntentosFallidos { get; set; }

        public Rol Rol { get; set; } = null!;
    }
}
