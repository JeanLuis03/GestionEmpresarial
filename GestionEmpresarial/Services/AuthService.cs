using GestionEmpresarial.DBContext;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using GestionEmpresarial.Models.Auth;
using GestionEmpresarial.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public AuthService(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<AuthResult> AutenticarAsync(LoginViewModel model)
        {
            var usuario = await _context.Usuarios
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.NombreUsuario == model.NombreUsuario && x.Activo);


            if (usuario == null)
            {
                return new AuthResult
                {
                    Exitoso = false,
                    Mensaje = "No posee acceso al sistema"
                };
            }

            if (usuario.Bloqueado)
            {
                return new AuthResult
                {
                    Exitoso = false,
                    Mensaje = "Usuario bloqueado. Contacte al administrador."
                };
            }

            var ContrasenaValida = _passwordService.VerifyPassword(model.Contrasena, usuario.ContrasenaHash);

            if (!ContrasenaValida)
            {
                usuario.IntentosFallidos++;
                if (usuario.IntentosFallidos == 3)
                {
                    usuario.Bloqueado = true;
                }
                await _context.SaveChangesAsync();

                return new AuthResult
                {
                    Exitoso = false,
                    Mensaje = "Credenciales incorrectas."
                };
            }

            usuario.IntentosFallidos = 0;
            usuario.Bloqueado = false;
            await _context.SaveChangesAsync();

            return new AuthResult
            {
                Exitoso = true,
                Usuario = usuario
            };

        }
    }
}
