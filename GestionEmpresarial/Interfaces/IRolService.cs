using GestionEmpresarial.Helpers.Responses;

namespace GestionEmpresarial.Interfaces
{
    public interface IRolService
    {
        Task<ApiResponse> ObtenerActivosComboAsync();
    }
}
