using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Enums;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Helpers
{
    public static class SeedData
    {
        public static async Task InicializarAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var passwordService =
                scope.ServiceProvider.GetRequiredService<IPasswordService>();

            await CrearRolesAsync(context);

            await CrearPermisosAsync(context);

            //await CrearPermisosRolAsync(context);

            //await CrearUsuariosAsync(context,passwordService);
        }

        private static async Task CrearRolesAsync(ApplicationDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var roles = new List<Rol>
            {
                new()
                {
                    Id = SystemGuids.IdRolAdministrador,
                    Nombre = "Administrador",
                    Descripcion = "Acceso total"
                },

                new()
                {
                    Id = SystemGuids.IdRolSupervisor,
                    Nombre = "Supervisor",
                    Descripcion = "Consulta y edición"
                },

                new()
                {
                    Id = SystemGuids.IdRolEjecutor,
                    Nombre = "Ejecutor",
                    Descripcion = "Consulta y creación"
                }
            };

            context.Roles.AddRange(roles);

            await context.SaveChangesAsync();
        }

        private static async Task CrearPermisosAsync(ApplicationDbContext context)
        {
            if (await context.Permisos.AnyAsync())
                return;

            var permisos = new List<Permiso>
            {
                new()
                {
                    Id = SystemGuids.IdPermisoAgregar,
                    Nombre = "Agregar"
                },

                new()
                {
                    Id = SystemGuids.IdPermisoEditar,
                    Nombre = "Editar"
                },

                new()
                {
                    Id = SystemGuids.IdPermisoEliminar,
                    Nombre = "Eliminar"
                },

                new()
                {
                    Id = SystemGuids.IdPermisoConsultar,
                    Nombre = "Consultar"
                }
            };

            context.Permisos.AddRange(permisos);

            await context.SaveChangesAsync();
        }




    }
}
