using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Helpers.Seed
{
    public class RolSeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;

        public RolSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public int OrdenEjecucion => 1;

        public async Task SeedAsync()
        {
            var roles = new[]
            {
                RolesSistema.Administrador,
                RolesSistema.Supervisor,
                RolesSistema.Ejecutor
            };

            foreach (var nombreRol in roles)
            {
                var existe = await _context.Roles.AnyAsync(r => r.Nombre == nombreRol);

                if (!existe)
                {
                    _context.Roles.Add(new Rol
                    {
                        Nombre = nombreRol
                    });
                }
            }
        }
    }
}
