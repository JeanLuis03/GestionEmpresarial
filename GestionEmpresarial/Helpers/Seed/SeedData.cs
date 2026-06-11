using GestionEmpresarial.DBContext;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpresarial.Helpers.Seed
{
    public static class SeedData
    {
        public static async Task InicializarAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var seeders = scope.ServiceProvider
                     .GetServices<ISeeder>()
                     .OrderBy(x => x.OrdenEjecucion)
                     .ToList();

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();

                await context.SaveChangesAsync();
            }
        }

    }
}
