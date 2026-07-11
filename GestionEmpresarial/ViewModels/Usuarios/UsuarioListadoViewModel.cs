namespace GestionEmpresarial.ViewModels.Usuarios
{
    public class UsuarioListadoViewModel
    {
        public Guid Id { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public DateTime UltimaFecha { get; set; }

        public bool Activo { get; set; }
    }
}
