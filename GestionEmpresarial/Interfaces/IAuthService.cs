using GestionEmpresarial.Models;
using GestionEmpresarial.ViewModels;

namespace GestionEmpresarial.Interfaces
{
    public interface IAuthService
    {
        Task<Usuario?> AutenticarAsync(LoginViewModel loginViewModel);
    }
}
