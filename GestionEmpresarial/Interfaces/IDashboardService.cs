using GestionEmpresarial.ViewModels;

namespace GestionEmpresarial.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> ObtenerDashboardAsync();
    }
}