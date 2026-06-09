using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmpresarial.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _authService.AutenticarAsync(model);
            if (usuario is null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
