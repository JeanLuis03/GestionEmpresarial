using GestionEmpresarial.Helpers.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    [Authorize]
    public class SeguridadController : Controller
    {
        [Authorize(Roles = RolesSistema.Administrador)]
        public IActionResult Administrador()
        {
            return Content("Área Administrador");
        }

        [Authorize(Roles = RolesSistema.Supervisor)]
        public IActionResult Supervisor()
        {
            return Content("Área Supervisor");
        }

        [Authorize(Roles = RolesSistema.Ejecutor)]
        public IActionResult Ejecutor()
        {
            return Content("Área Ejecutor");
        }

    }
}
