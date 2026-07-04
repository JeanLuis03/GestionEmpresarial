using GestionEmpresarial.Attributes;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    [PermissionAuthorize(PermisosSistema.Consultar)]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolService;

        public UsuarioController(
            IUsuarioService usuarioService,
            IRolService rolService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _usuarioService.ObtenerTodosAsync();

            return Ok(resultado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var resultado = await _usuarioService.ObtenerPorIdAsync(id);

            if (!resultado.Success)
            {
                return NotFound();
            }

            return Ok(resultado.Data);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRolesActivosCombo()
        {
            var resultado = await _rolService.ObtenerActivosComboAsync();

            return Ok(resultado.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar([FromBody] UsuarioGuardarViewModel model)
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

            var resultado = await _usuarioService.GuardarAsync(model);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(PermisosSistema.Eliminar)]
        public async Task<IActionResult> CambiarEstado([FromBody] UsuarioCambiarEstadoViewModel model)
        {
            var resultado = await _usuarioService.CambiarEstadoAsync(model.Id);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }
    }
}
