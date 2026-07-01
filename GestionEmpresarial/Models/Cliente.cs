using System.ComponentModel.DataAnnotations;

namespace GestionEmpresarial.Models
{
    public class Cliente : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Direccion { get; set; } = string.Empty;
    }
}
