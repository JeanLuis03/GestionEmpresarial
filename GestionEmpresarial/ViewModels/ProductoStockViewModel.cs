namespace GestionEmpresarial.ViewModels
{
    public class ProductoStockViewModel
    {
        public string Codigo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public int Stock { get; set; }
    }
}