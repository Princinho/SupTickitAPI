using Suptickit.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    internal static class Utils
    {
        public static async Task<bool> hasRole(int userId, int roleId, IRoleAssignmentRepository rolesRepo)
        {
            var userRoles = await rolesRepo.GetByUserIdAsync(userId);
            return userRoles.Any(r => r.Id == roleId && r.StartDate < DateTime.UtcNow && r.ExpiryDate > DateTime.UtcNow);
        }
    }
}
