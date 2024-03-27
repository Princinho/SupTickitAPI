using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IPartRepository
    {
        public Task<ServiceResponse<IEnumerable<Part>>> GetAllAsync();
        public Task<ServiceResponse<Part>> AddAsync(Part service);
        public Task<ServiceResponse<Part>> RemoveAsync(int id);
        public Task<ServiceResponse<Part>> UpdateAsync(Part service);
        public Task<ServiceResponse<Part>> GetByIdAsync(int id);
    }
}
