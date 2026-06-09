using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionEmpresarial.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(new Usuario(),password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(
            new Usuario(),
            passwordHash,
            password);

            return result == PasswordVerificationResult.Success;
        }


    }
}
