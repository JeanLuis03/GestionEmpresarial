using GestionEmpresarial.Helpers.Responses;
using GestionEmpresarial.ViewModels.Clientes;

namespace GestionEmpresarial.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteListadoViewModel>> ObtenerListadoAsync();

        Task<ClienteDetalleViewModel?> ObtenerPorIdAsync(Guid id);

        Task<ApiResponse> GuardarAsync(ClienteGuardarViewModel model);

        Task<ApiResponse> CambiarEstadoAsync(Guid id);

    }
}
