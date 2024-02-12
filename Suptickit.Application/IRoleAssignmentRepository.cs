﻿using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IRoleAssignmentRepository
    {
        public Task<IEnumerable<RoleAssignment>> GetAllAsync();
        public Task<IEnumerable<RoleAssignment>> GetAllActiveAsync();
        public Task<IEnumerable<RoleAssignment>> GetByUserIdAsync(int userId);
        public Task<RoleAssignment> AddAsync(RoleAssignment roleAssignment);
        public Task<RoleAssignment> UpdateAsync(RoleAssignment roleAssignment);
        public Task<RoleAssignment> DeleteAsync(RoleAssignment roleAssignment);
        public Task<ServiceResponse<RoleAssignment>> AssignRole(RoleAssignment roleAssignment);
        public Task<ServiceResponse<bool>> UnAssignRole(int id);
    }
}
