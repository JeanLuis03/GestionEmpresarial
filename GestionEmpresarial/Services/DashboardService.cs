using GestionEmpresarial.DBContext;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DashboardService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<DashboardViewModel> ObtenerDashboardAsync()
        {
            var totalClientes = await _context.Clientes
                .AsNoTracking()
                .CountAsync(c => c.Activo);

            var totalProductos = await _context.Productos
                .AsNoTracking()
                .CountAsync(p => p.Activo);

            var totalUsuarios = await _context.Usuarios
                .AsNoTracking()
                .CountAsync(u => u.Activo);

            var totalCategorias = await _context.Categorias
                .AsNoTracking()
                .CountAsync(c => c.Activo);

            var categorias = await _context.Productos
                .AsNoTracking()
                .Where(p => p.Activo && p.Categoria.Activo)
                .GroupBy(p => p.Categoria.Nombre)
                .Select(g => new CategoriaDashboardViewModel
                {
                    Categoria = g.Key,
                    CantidadProductos = g.Count()
                })
                .OrderByDescending(x => x.CantidadProductos)
                .ThenBy(x => x.Categoria)
                .ToListAsync();

            var productosBajoStock = await _context.Productos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .Where(p => p.Activo)
                .OrderBy(p => p.Stock)
                .ThenBy(p => p.Codigo)
                .Take(5)
                .Select(p => new ProductoStockViewModel
                {
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    Categoria = p.Categoria.Nombre,
                    Stock = p.Stock
                })
                .ToListAsync();

            return new DashboardViewModel
            {
                TotalClientes = totalClientes,
                TotalProductos = totalProductos,
                TotalUsuarios = totalUsuarios,
                TotalCategorias = totalCategorias,
                Categorias = categorias,
                ProductosBajoStock = productosBajoStock,
                FechaActual = DateTime.Now,
                NombreUsuario = _currentUserService.NombreUsuario,
                RolUsuario = _currentUserService.Rol
            };
        }
    }
}