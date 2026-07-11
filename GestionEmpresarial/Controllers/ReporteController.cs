using GestionEmpresarial.Attributes;
using GestionEmpresarial.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    [PermissionAuthorize(PermisosSistema.Consultar)]
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }
    }
}