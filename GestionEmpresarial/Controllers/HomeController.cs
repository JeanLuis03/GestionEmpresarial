using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace GestionEmpresarial.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICurrentUserService _currentUserService;

        public HomeController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                Usuario = _currentUserService.NombreUsuario,
                Rol = _currentUserService.Rol,
                FechaActual = DateTime.Now
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
