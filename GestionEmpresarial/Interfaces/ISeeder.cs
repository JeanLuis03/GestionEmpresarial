namespace GestionEmpresarial.Interfaces
{
    public interface ISeeder
    {
        int OrdenEjecucion { get; }
        Task SeedAsync();
    }
}
