using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Suptickit.Domain.Enums;
using Suptickit.Infrastructure;

namespace SupTickit.API.CustomAttributes
{
    public class AgentLevelAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _db = context.HttpContext.RequestServices.GetService(typeof(SuptickitContext)) as SuptickitContext;
            var username = context.HttpContext.User.Claims.First(c => c.Type == "username");
            var dbUser = _db.Users.Where(u => u.Username == username.Value).FirstOrDefault();
            var userRoleAssignments = _db.RoleAssignments.Where(r => r.UserId == dbUser.Id).ToList();
            if (!userRoleAssignments.Any(
                assignment => assignment.RoleId <= RoleEnum.Agent && assignment.StartDate < DateTime.UtcNow && assignment.ExpiryDate > DateTime.UtcNow))
            {
                context.Result = new UnauthorizedResult();
            }
            Console.WriteLine(userRoleAssignments);
        }
    }
}
