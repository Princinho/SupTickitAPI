using Suptickit.Domain.Models;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IPartCategoryRepository
    {

        public Task<ServiceResponse<IEnumerable<PartCategory>>> GetAllAsync();
        public Task<ServiceResponse<PartCategory>> AddAsync(PartCategory category);
        public Task<ServiceResponse<PartCategory>> RemoveAsync(int id);
        public Task<ServiceResponse<PartCategory>> UpdateAsync(PartCategory cat);
        public Task<ServiceResponse<PartCategory>> GetByIdAsync(int id);
    }
}
