using Microsoft.AspNetCore.Authorization;

namespace GestionEmpresarial.Attributes
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionAuthorizeAttribute(string permission)
        {
            Policy = permission;
        }

    }
}
