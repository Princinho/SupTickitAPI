using Microsoft.EntityFrameworkCore;
using Suptickit.Application;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class RoleAssignmentsRepository : IRoleAssignmentRepository
    {
        private readonly SuptickitContext _db;
        public RoleAssignmentsRepository(SuptickitContext db)
        {
            _db = db;
        }

        public async Task<RoleAssignment> AddAsync(RoleAssignment roleAssignment)
        {
            roleAssignment.DateCreated = DateTime.UtcNow;
            _db.RoleAssignments.Add(roleAssignment);
            await _db.SaveChangesAsync();
            return roleAssignment;
        }

        public async Task<RoleAssignment> DeleteAsync(RoleAssignment roleAssignment)
        {
            var dbEntry = await _db.RoleAssignments.Where(r => r.UserId == roleAssignment.UserId && r.RoleId == roleAssignment.RoleId).FirstOrDefaultAsync();
            if(dbEntry != null)
            {
                _db.Remove(dbEntry);
            await _db.SaveChangesAsync();
            }
            return dbEntry;
        }

        public async Task<IEnumerable<RoleAssignment>> GetByUserIdAsync(int userId)
        {
            return await _db.RoleAssignments.Where(r => r.UserId == userId).ToListAsync();
        }
        public async Task<IEnumerable<RoleAssignment>> GetAllAsync()
        {
            return await _db.RoleAssignments.ToListAsync();
        }
        public async Task<IEnumerable<RoleAssignment>> GetAllActiveAsync()
        {
            return await _db.RoleAssignments.Where(r=>r.StartDate<DateTime.UtcNow && r.ExpiryDate>DateTime.UtcNow).ToListAsync();
        }

        public async Task<RoleAssignment> UpdateAsync(RoleAssignment roleAssignment)
        {
            var dbEntry = await _db.RoleAssignments.Where(r => r.UserId == roleAssignment.UserId && r.RoleId == roleAssignment.RoleId).FirstOrDefaultAsync();
            _db.Update(roleAssignment);
            await _db.SaveChangesAsync();
            return dbEntry;
        }

        public async Task<Application.ServiceResponse<RoleAssignment>> AssignRole(RoleAssignment roleAssignment)
        {
            var user = _db.Users.Include(u => u.RoleAssignments).FirstOrDefault();
            if (user == null) { return new Application.ServiceResponse<RoleAssignment> { Message = "User does not exist", Success = false }; }
            _db.RoleAssignments.Add(roleAssignment);
            await _db.SaveChangesAsync();
            return new Application.ServiceResponse<RoleAssignment> { Message = "Successfully assigned role to user", Success = true, Data = roleAssignment };
        }


        public async Task<Application.ServiceResponse<bool>> UnAssignRole(int id)
        {
            var role = await _db.RoleAssignments.FindAsync(id);
            _db.RoleAssignments.Remove(role);
            await _db.SaveChangesAsync();
            return new Application.ServiceResponse<bool> { Success=true, Data = true, Message = "Successfully unassigned role" };
        }
    }
}
