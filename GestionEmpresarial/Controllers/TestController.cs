using GestionEmpresarial.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    public class TestController : Controller
    {
        private readonly ICurrentUserService _currentUserService;

        public TestController(
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public IActionResult Index()
        {
            return Content(
                $"Id: {_currentUserService.UsuarioId}\n" +
                $"Usuario: {_currentUserService.NombreUsuario}\n" +
                $"Rol: {_currentUserService.Rol}");
        }
    }
}
