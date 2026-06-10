using System.Security.Claims;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        //[Route("Login")]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await _authService.AutenticarAsync(model);

            if (!resultado.Exitoso)
            {
                TempData["SwalType"] = "error";
                TempData["SwalTitle"] = "Error de autenticación";
                TempData["SwalMessage"] = resultado.Mensaje;

                return RedirectToAction(nameof(Index));
            }

            var usuario = resultado.Usuario!;

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    usuario.NombreUsuario),

                new Claim(
                    ClaimTypes.Role,
                    usuario.Rol.Nombre)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false  
                });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            TempData["SwalType"] = "warning";

            TempData["SwalTitle"] = "Acceso denegado";

            TempData["SwalMessage"] =
                "No posee permisos suficientes para acceder a este apartado.";

            return RedirectToAction("Index", "Home");
        }

    }
}
