namespace GestionEmpresarial.ViewModels.Usuarios
{
    public class UsuarioDetalleViewModel
    {
        public Guid Id { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public Guid IdRol { get; set; }

        public string Rol { get; set; } = string.Empty;

        public bool Activo { get; set; }
    }
}
