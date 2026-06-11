using GestionEmpresarial.Models.Auth;
using GestionEmpresarial.ViewModels;

namespace GestionEmpresarial.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AutenticarAsync(LoginViewModel model);
    }
}
