using GestionEmpresarial.Attributes;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels.Productos;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    [PermissionAuthorize(PermisosSistema.Consultar)]
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _productoService.ObtenerTodosAsync();

            return Ok(resultado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var resultado = await _productoService.ObtenerPorIdAsync(id);

            if (!resultado.Success)
            {
                return NotFound();
            }

            return Ok(resultado.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar([FromBody] ProductoGuardarViewModel model)
        {
            if (!User.HasClaim(
                    ClaimTypesSistema.Permission,
                    model.Id.HasValue
                        ? PermisosSistema.Editar
                        : PermisosSistema.Agregar))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(
                    ApiResponse.Fail(
                        string.Join("<br>", errores)));
            }

            var resultado = await _productoService.GuardarAsync(model);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(PermisosSistema.Eliminar)]
        public async Task<IActionResult> Eliminar([FromBody] ProductoEliminarViewModel model)
        {
            var resultado = await _productoService.CambiarEstadoAsync(model.Id);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
