namespace GestionEmpresarial.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UsuarioId { get; }
        string NombreUsuario { get; }
        string Rol { get; }
        bool EstaAutenticado { get; }
        bool EsAdministrador { get; }
        bool EsSupervisor { get; }
        bool EsEjecutor { get; }
    }
}
