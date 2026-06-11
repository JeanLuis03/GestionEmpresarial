using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Constants;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Helpers.Seed
{
    public class PermisoRolSeeder : ISeeder
    {
        private readonly ApplicationDbContext _context;

        public PermisoRolSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public int OrdenEjecucion => 3;

        public async Task SeedAsync()
        {
            //ROLES 
            var administrador = await _context.Roles
                .FirstAsync(r => r.Nombre == RolesSistema.Administrador);

            var supervisor = await _context.Roles
                .FirstAsync(r => r.Nombre == RolesSistema.Supervisor);

            var ejecutor = await _context.Roles
                .FirstAsync(r => r.Nombre == RolesSistema.Ejecutor);


            //PERMISOS
            var agregar = await _context.Permisos
                .FirstAsync(p => p.Nombre == PermisosSistema.Agregar);

            var editar = await _context.Permisos
                .FirstAsync(p => p.Nombre == PermisosSistema.Editar);

            var eliminar = await _context.Permisos
                .FirstAsync(p => p.Nombre == PermisosSistema.Eliminar);

            var consultar = await _context.Permisos
                .FirstAsync(p => p.Nombre == PermisosSistema.Consultar);

            //RELACION ROL-PERMISO
            await CrearRelacion(administrador.Id, agregar.Id);
            await CrearRelacion(administrador.Id, editar.Id);
            await CrearRelacion(administrador.Id, eliminar.Id);
            await CrearRelacion(administrador.Id, consultar.Id);

            await CrearRelacion(supervisor.Id, editar.Id);
            await CrearRelacion(supervisor.Id, consultar.Id);

            await CrearRelacion(ejecutor.Id, agregar.Id);
            await CrearRelacion(ejecutor.Id, consultar.Id);
        }

        private async Task CrearRelacion(Guid idRol, Guid idPermiso)
        {
            var existe = await _context.PermisosRoles.AnyAsync(x =>
                x.IdRol == idRol &&
                x.IdPermiso == idPermiso);

            if (!existe)
            {
                _context.PermisosRoles.Add(new PermisosRol
                {
                    IdRol = idRol,
                    IdPermiso = idPermiso
                });
            }
        }


    }
}
