using SchoolNewspaperBlazorApp.Data;
using System.Security.Claims;

namespace SchoolNewspaperBlazorApp.Interfaces.Service
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(EnumPermissions permission);
        Task SavePermissionsAsync(List<EnumPermissions> permissions, int userId);
    }
}
