using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Helpers.Seed
{
    public class UsuarioSeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public UsuarioSeeder(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public int OrdenEjecucion => 4;

        public async Task SeedAsync()
        {
            await CrearUsuario(
                "admin",
                "admin@gestionempresarial.com",
                "Admin123*",
                RolesSistema.Administrador);

            await CrearUsuario(
                "supervisor",
                "supervisor@gestionempresarial.com",
                "Supervisor123*",
                RolesSistema.Supervisor);

            await CrearUsuario(
                "ejecutor",
                "ejecutor@gestionempresarial.com",
                "Ejecutor123*",
                RolesSistema.Ejecutor);
        }

        private async Task CrearUsuario(string nombreUsuario, string correo, string password, string nombreRol)
        {
            var existe = await _context.Usuarios
                .AnyAsync(u => u.NombreUsuario == nombreUsuario);

            if (existe)
                return;

            var rol = await _context.Roles
                .FirstAsync(r => r.Nombre == nombreRol);

            _context.Usuarios.Add(new Usuario
            {
                NombreUsuario = nombreUsuario,
                Correo = correo,
                ContrasenaHash = _passwordService.HashPassword(password),
                IdRol = rol.Id,
                IntentosFallidos = 0,
                Bloqueado = false
            });
        }

    }
}
