namespace GestionEmpresarial.ViewModels.Clientes
{
    public class ClienteListadoViewModel
    {
        public Guid Id { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;
    }
}
