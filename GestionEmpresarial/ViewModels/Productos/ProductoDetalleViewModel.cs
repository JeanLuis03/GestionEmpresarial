namespace GestionEmpresarial.ViewModels.Productos
{
    public class ProductoDetalleViewModel
    {
        public Guid Id { get; set; }

        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Marca { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Stock { get; set; }

        public Guid CategoriaId { get; set; }

        public string Categoria { get; set; } = string.Empty;
    }
}
