using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.ViewModels.Categorias;

namespace GestionEmpresarial.Interfaces
{
    public interface ICategoriaService
    {
        Task<ApiResponse> ObtenerTodosAsync();

        Task<ApiResponse> ObtenerPorIdAsync(Guid id);

        Task<ApiResponse> GuardarAsync(CategoriaGuardarViewModel model);

        Task<ApiResponse> CambiarEstadoAsync(Guid id);
    }
}
