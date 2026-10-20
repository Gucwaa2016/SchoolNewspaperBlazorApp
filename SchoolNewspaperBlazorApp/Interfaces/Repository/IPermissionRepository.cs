using SchoolNewspaperBlazorApp.Data;
using System.Security.Claims;

namespace SchoolNewspaperBlazorApp.Interfaces.Repository
{
    public interface IPermissionRepository
    {
        Task<bool> HasPermissionAsync(ClaimsPrincipal principal, EnumPermissions permission);
        Task SavePermissionsAsync(List<EnumPermissions> permissions, int userId);
    }
}
