using GestionEmpresarial.Attributes;
using GestionEmpresarial.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestionEmpresarial.Controllers
{
    [PermissionAuthorize(PermisosSistema.Consultar)]
    public class ReporteController : Controller
    {
        private readonly IConfiguration _configuration;

        public ReporteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [PermissionAuthorize(PermisosSistema.Consultar)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos()
        {
            var servidor = _configuration["Reportes:Servidor"];
            var url = $"{servidor}?/Productos&rs:Command=Render";
            return Redirect(url);
        }
    }
}