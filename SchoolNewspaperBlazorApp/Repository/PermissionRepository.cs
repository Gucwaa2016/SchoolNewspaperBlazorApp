using Microsoft.EntityFrameworkCore;
using SchoolNewspaperBlazorApp.Data;
using SchoolNewspaperBlazorApp.Interfaces.Repository;
using System.Security.Claims;

namespace SchoolNewspaperBlazorApp.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IDbContextFactory<NewspaperDbContext> _contextFactory;
        public PermissionRepository(IDbContextFactory<NewspaperDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<bool> HasPermissionAsync(ClaimsPrincipal principal, EnumPermissions permission)
        {

            var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            if(!int.TryParse(id, out var permissionId))
            {
                return false;
            }
            await using var _context = await _contextFactory.CreateDbContextAsync();
            return await _context.UserPermissions
                .AnyAsync(up => up.UserId == permissionId &&
                up.Permission.PermissionName == permission);
        }
    }
}
