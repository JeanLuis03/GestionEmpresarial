using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Helpers.Seed
{
    public class PermisoSeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;

        public PermisoSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public int OrdenEjecucion => 2;

        public async Task SeedAsync()
        {
            var permisos = new[]
            {
                PermisosSistema.Agregar,
                PermisosSistema.Editar,
                PermisosSistema.Eliminar,
                PermisosSistema.Consultar
            };

            foreach (var nombrePermiso in permisos)
            {
                var existe = await _context.Permisos.AnyAsync(p => p.Nombre == nombrePermiso);

                if (!existe)
                {
                    _context.Permisos.Add(
                    new Permiso
                    {
                        Nombre = nombrePermiso
                    });
                }
            }
        }
    }
}
