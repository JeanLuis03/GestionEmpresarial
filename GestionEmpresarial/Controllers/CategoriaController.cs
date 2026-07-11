using GestionEmpresarial.Attributes;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels.Categorias;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    [PermissionAuthorize(PermisosSistema.Consultar)]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _categoriaService.ObtenerTodosAsync();

            return Ok(resultado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerActivasCombo()
        {
            var resultado = await _categoriaService.ObtenerActivasComboAsync();

            return Ok(resultado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var resultado = await _categoriaService.ObtenerPorIdAsync(id);

            if (!resultado.Success)
            {
                return NotFound();
            }

            return Ok(resultado.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar([FromBody] CategoriaGuardarViewModel model)
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

            var resultado = await _categoriaService.GuardarAsync(model);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(PermisosSistema.Eliminar)]
        public async Task<IActionResult> Eliminar([FromBody] CategoriaEliminarViewModel model)
        {
            var resultado = await _categoriaService.CambiarEstadoAsync(model.Id);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}