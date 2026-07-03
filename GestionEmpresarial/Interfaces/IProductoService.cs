using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.ViewModels.Productos;

namespace GestionEmpresarial.Interfaces
{
    public interface IProductoService
    {
        Task<ApiResponse> ObtenerTodosAsync();

        Task<ApiResponse> ObtenerPorIdAsync(Guid id);

        Task<ApiResponse> GuardarAsync(ProductoGuardarViewModel model);

        Task<ApiResponse> CambiarEstadoAsync(Guid id);
    }
}
