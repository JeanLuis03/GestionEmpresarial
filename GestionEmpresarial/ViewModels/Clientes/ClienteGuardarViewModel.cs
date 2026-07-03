using System.ComponentModel.DataAnnotations;

namespace GestionEmpresarial.ViewModels.Clientes
{
    public class ClienteGuardarViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100,
            ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100,
            ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression( @"^\d{3}-\d{3}-\d{4}$",
            ErrorMessage = "El teléfono debe tener el formato 809-555-1234.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(
            ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [StringLength(150,
            ErrorMessage = "El correo no puede superar los 150 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(250,
            ErrorMessage = "La dirección no puede superar los 250 caracteres.")]
        public string Direccion { get; set; } = string.Empty;
    }
}
