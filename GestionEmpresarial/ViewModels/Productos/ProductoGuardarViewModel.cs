using System.ComponentModel.DataAnnotations;

namespace GestionEmpresarial.ViewModels.Productos
{
    public class ProductoGuardarViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede superar los 20 caracteres.")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [StringLength(100, ErrorMessage = "La marca no puede superar los 100 caracteres.")]
        public string Marca { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "El modelo no puede superar los 100 caracteres.")]
        public string? Modelo { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public Guid? CategoriaId { get; set; }
    }
}
