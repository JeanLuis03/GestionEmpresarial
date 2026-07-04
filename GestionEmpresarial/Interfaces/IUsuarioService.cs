using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.ViewModels.Usuarios;

namespace GestionEmpresarial.Interfaces
{
    public interface IUsuarioService
    {
        Task<ApiResponse> ObtenerTodosAsync();

        Task<ApiResponse> ObtenerPorIdAsync(Guid id);

        Task<ApiResponse> GuardarAsync(UsuarioGuardarViewModel model);

        Task<ApiResponse> CambiarEstadoAsync(Guid id);
    }
}
