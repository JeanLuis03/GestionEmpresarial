using System.ComponentModel.DataAnnotations;

namespace GestionEmpresarial.ViewModels.Categorias
{
    public class CategoriaGuardarViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100,
            ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
    }
}