namespace GestionEmpresarial.Models.Auth
{
    public class AuthResult
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public Usuario? Usuario { get; set; }

    }
}
