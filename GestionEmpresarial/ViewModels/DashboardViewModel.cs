namespace GestionEmpresarial.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalClientes { get; set; }

        public int TotalProductos { get; set; }

        public int TotalUsuarios { get; set; }

        public int TotalCategorias { get; set; }

        public List<CategoriaDashboardViewModel> Categorias { get; set; } = new();

        public List<ProductoStockViewModel> ProductosBajoStock { get; set; } = new();

        public DateTime FechaActual { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string RolUsuario { get; set; } = string.Empty;
    }
}
