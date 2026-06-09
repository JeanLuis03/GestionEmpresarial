using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels;

namespace GestionEmpresarial.Services
{
    public class AuthService : IAuthService
    {
        public Task<Usuario?> AutenticarAsync(LoginViewModel loginViewModel)
        {
            return Task.FromResult<Usuario?>(null);
        }
    }
}
