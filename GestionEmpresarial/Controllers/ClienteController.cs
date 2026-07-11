using GestionEmpresarial.Attributes;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    [PermissionAuthorize(PermisosSistema.Consultar)]
    public class ClienteController : Controller
    {

        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerListado()
        {
            var clientes = await _clienteService.ObtenerListadoAsync();

            return Ok(clientes);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(Guid id)
        {
            var cliente =
                await _clienteService.ObtenerPorIdAsync(id);

            if (cliente is null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar([FromBody] ClienteGuardarViewModel model)
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

            var resultado =
                await _clienteService.GuardarAsync(model);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionAuthorize(PermisosSistema.Eliminar)]
        public async Task<IActionResult> Eliminar([FromBody] ClienteEliminarViewModel model)
        {
            var resultado = await _clienteService.CambiarEstadoAsync(model.Id);

            if (!resultado.Success)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado);
        }

    }
}
