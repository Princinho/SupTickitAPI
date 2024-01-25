using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IUsersRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<ServiceResponse<User>> RemoveAsync(int id);
        public Task<ServiceResponse<RoleAssignment>> AssignRole(RoleAssignment roleAssignment);
        public Task<User> GetByName(string name);
    }
}
