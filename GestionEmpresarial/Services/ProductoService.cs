using AutoMapper;
using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Productos;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class ProductoService : IProductoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Private Methods
        private async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<bool> ExisteCodigoAsync(string codigo, Guid? productoId = null)
        {
            return await _context.Productos.AnyAsync(p =>
                p.Codigo == codigo &&
                (!productoId.HasValue || p.Id != productoId.Value));
        }

        private async Task<bool> ExisteCategoriaAsync(Guid categoriaId)
        {
            return await _context.Categorias.AnyAsync(c =>
                c.Id == categoriaId &&
                c.Activo);
        }

        private async Task<ApiResponse> CrearProductoAsync(ProductoGuardarViewModel model)
        {
            var producto = _mapper.Map<Producto>(model);

            producto.Id = Guid.NewGuid();
            producto.Activo = true;
            producto.FechaCreacion = DateTime.Now;
            producto.FechaModificacion = null;

            await _context.Productos.AddAsync(producto);
            await GuardarCambiosAsync();

            return ApiResponse.Ok("Producto registrado correctamente.");
        }

        private async Task<ApiResponse> ActualizarProductoAsync(ProductoGuardarViewModel model)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p =>
                p.Id == model.Id &&
                p.Activo);

            if (producto is null)
            {
                return ApiResponse.Fail("No se encontró el producto.");
            }

            _mapper.Map(model, producto);
            producto.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Producto actualizado correctamente.");
        }
        #endregion

        public async Task<ApiResponse> ObtenerTodosAsync()
        {
            var productos = await _context.Productos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .Where(p => p.Activo)
                .OrderBy(p => p.Codigo)
                .ToListAsync();

            var resultado = _mapper.Map<IEnumerable<ProductoListadoViewModel>>(productos);

            return ApiResponse.Ok(data: resultado);
        }

        public async Task<ApiResponse> ObtenerPorIdAsync(Guid id)
        {
            var producto = await _context.Productos
                .AsNoTracking()
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p =>
                    p.Id == id &&
                    p.Activo);

            if (producto is null)
            {
                return ApiResponse.Fail("No se encontró el producto.");
            }

            return ApiResponse.Ok(data: _mapper.Map<ProductoDetalleViewModel>(producto));
        }

        public async Task<ApiResponse> GuardarAsync(ProductoGuardarViewModel model)
        {
            model.Codigo = model.Codigo.Trim();
            model.Nombre = model.Nombre.Trim();
            model.Marca = model.Marca.Trim();
            model.Modelo = model.Modelo?.Trim();

            if (string.IsNullOrWhiteSpace(model.Codigo))
            {
                return ApiResponse.Fail("El código es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                return ApiResponse.Fail("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(model.Marca))
            {
                return ApiResponse.Fail("La marca es obligatoria.");
            }

            if (model.Precio <= 0)
            {
                return ApiResponse.Fail("El precio debe ser mayor que cero.");
            }

            if (decimal.Round(model.Precio, 2) != model.Precio)
            {
                return ApiResponse.Fail("El precio no puede tener más de dos decimales.");
            }

            if (model.Stock < 0)
            {
                return ApiResponse.Fail("El stock no puede ser negativo.");
            }

            if (!model.CategoriaId.HasValue || model.CategoriaId == Guid.Empty)
            {
                return ApiResponse.Fail("La categoría es obligatoria.");
            }

            if (!await ExisteCategoriaAsync(model.CategoriaId.Value))
            {
                return ApiResponse.Fail("La categoría seleccionada no existe.");
            }

            if (await ExisteCodigoAsync(model.Codigo, model.Id))
            {
                return ApiResponse.Fail("Ya existe un producto con ese código.");
            }

            if (model.Id.HasValue)
            {
                return await ActualizarProductoAsync(model);
            }

            return await CrearProductoAsync(model);
        }

        public async Task<ApiResponse> CambiarEstadoAsync(Guid id)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p =>
                    p.Id == id &&
                    p.Activo);

            if (producto is null)
            {
                return ApiResponse.Fail("El producto no fue encontrado.");
            }

            producto.Activo = false;
            producto.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Producto eliminado correctamente.");
        }
    }
}
