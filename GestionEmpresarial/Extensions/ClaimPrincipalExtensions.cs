using GestionEmpresarial.Helpers.Constants;
using System.Security.Claims;

namespace GestionEmpresarial.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static bool TienePermiso(this ClaimsPrincipal user, string permiso)
        {
            return user.HasClaim(
                ClaimTypesSistema.Permission,
                permiso);
        }
    }
}
