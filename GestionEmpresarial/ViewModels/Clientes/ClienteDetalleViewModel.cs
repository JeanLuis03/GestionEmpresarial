namespace GestionEmpresarial.ViewModels.Clientes
{
    public class ClienteDetalleViewModel
    {
        public Guid Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;
    }
}
