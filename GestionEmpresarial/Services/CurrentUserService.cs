using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Interfaces;
using System.Security.Claims;

namespace GestionEmpresarial.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? Usuario => _httpContextAccessor.HttpContext?.User;
        public bool EstaAutenticado => Usuario?.Identity?.IsAuthenticated ?? false;

        public Guid UsuarioId
        {
            get
            {
                var valor = Usuario?.FindFirstValue(ClaimTypes.NameIdentifier);

                return Guid.TryParse(valor, out var id)
                    ? id
                    : Guid.Empty;
            }
        }

        public string NombreUsuario =>
            Usuario?.FindFirstValue(ClaimTypes.Name)
            ?? string.Empty;

        public string Rol =>
            Usuario?.FindFirstValue(ClaimTypes.Role)
            ?? string.Empty;

        public bool EsAdministrador => Rol == RolesSistema.Administrador;

        public bool EsSupervisor => Rol == RolesSistema.Supervisor;

        public bool EsEjecutor => Rol == RolesSistema.Ejecutor;
    }
}
