using Suptickit.Application;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public static class Utils
    {
        public static async Task<bool> hasRole(int userId, int roleId, IRoleAssignmentRepository rolesRepo)
        {
            var userRoles = await rolesRepo.GetByUserIdAsync(userId);
            return userRoles.Any(r => r.Id == roleId && r.StartDate < DateTime.UtcNow && r.ExpiryDate > DateTime.UtcNow);
        }
        public static TicketLog getTicketLog(Ticket ticket)
        {
            var ticketLog = new TicketLog
            {
                TicketId = ticket.Id,
                CreatedBy = ticket.CreatedBy,
                CategoryId = ticket.CategoryId,
                AssignedBy = ticket.AssignedBy,
                AgentId = ticket.AgentId,
                Description = ticket.Description,
                ProductReference = ticket.ProductReference,
                DateCreated = ticket.DateCreated,
                Name = ticket.Name,
                Priority = ticket.Priority,
                Status = ticket.Status,
                Ticket = ticket
            };
            return ticketLog;
        }
        public static async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
        public static string getUsername(ClaimsPrincipal user)
        {
            return user.Claims.First(c => c.Type == "username").Value;
        }
    }

}
