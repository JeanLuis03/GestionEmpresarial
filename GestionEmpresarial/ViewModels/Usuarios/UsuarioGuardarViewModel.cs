using System.ComponentModel.DataAnnotations;

namespace GestionEmpresarial.ViewModels.Usuarios
{
    public class UsuarioGuardarViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El usuario no puede superar los 50 caracteres.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "La contraseña no puede superar los 100 caracteres.")]
        public string? Contrasena { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        public Guid? IdRol { get; set; }
    }
}
