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

        public async Task<RoleAssignment> UpdateAsync(RoleAssignment roleAssignment)
        {
            var dbEntry = await _db.RoleAssignments.Where(r => r.UserId == roleAssignment.UserId && r.RoleId == roleAssignment.RoleId).FirstOrDefaultAsync();
            _db.Update(roleAssignment);
            await _db.SaveChangesAsync();
            return dbEntry;
        }
    }
}
