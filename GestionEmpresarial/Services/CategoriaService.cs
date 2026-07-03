using AutoMapper;
using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels.Categorias;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Private Methods
        private async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        private async Task<bool> ExisteNombreAsync(string nombre, Guid? categoriaId = null)
        {
            return await _context.Categorias.AnyAsync(c =>
                c.Nombre == nombre &&
                (!categoriaId.HasValue || c.Id != categoriaId.Value));
        }

        private async Task<ApiResponse> CrearCategoriaAsync(CategoriaGuardarViewModel model)
        {
            var categoria = _mapper.Map<Categoria>(model);

            categoria.Id = Guid.NewGuid();
            categoria.Activo = true;
            categoria.FechaCreacion = DateTime.Now;
            categoria.FechaModificacion = null;

            await _context.Categorias.AddAsync(categoria);

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Categoría registrada correctamente.");
        }

        private async Task<ApiResponse> ActualizarCategoriaAsync(CategoriaGuardarViewModel model)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(c =>
                c.Id == model.Id &&
                c.Activo);

            if (categoria is null)
            {
                return ApiResponse.Fail("No se encontró la categoría.");
            }

            _mapper.Map(model, categoria);
            categoria.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Categoría actualizada correctamente.");
        }
        #endregion

        public async Task<ApiResponse> ObtenerTodosAsync()
        {
            var categorias = await _context.Categorias
                .AsNoTracking()
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            var resultado = _mapper.Map<IEnumerable<CategoriaListadoViewModel>>(categorias);

            return ApiResponse.Ok(data: resultado);
        }

        public async Task<ApiResponse> ObtenerPorIdAsync(Guid id)
        {
            var categoria = await _context.Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.Activo);

            if (categoria is null)
            {
                return ApiResponse.Fail("No se encontró la categoría.");
            }

            return ApiResponse.Ok(data: _mapper.Map<CategoriaDetalleViewModel>(categoria));
        }

        public async Task<ApiResponse> GuardarAsync(CategoriaGuardarViewModel model)
        {
            model.Nombre = model.Nombre.Trim();

            if (string.IsNullOrWhiteSpace(model.Nombre))
            {
                return ApiResponse.Fail("El nombre es obligatorio.");
            }

            if (await ExisteNombreAsync(model.Nombre, model.Id))
            {
                return ApiResponse.Fail("Ya existe una categoría con ese nombre.");
            }

            if (model.Id.HasValue)
            {
                return await ActualizarCategoriaAsync(model);
            }

            return await CrearCategoriaAsync(model);
        }

        public async Task<ApiResponse> CambiarEstadoAsync(Guid id)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    c.Activo);

            if (categoria is null)
            {
                return ApiResponse.Fail("La categoría no fue encontrada.");
            }

            categoria.Activo = false;
            categoria.FechaModificacion = DateTime.Now;

            await GuardarCambiosAsync();

            return ApiResponse.Ok("Categoría eliminada correctamente.");
        }
    }
}