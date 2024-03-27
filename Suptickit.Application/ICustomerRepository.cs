using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ICustomerRepository
    {
        public Task<ServiceResponse<IEnumerable<Customer>>> GetAllAsync();
        public Task<ServiceResponse<Customer>> AddAsync(Customer customer);
        public Task<ServiceResponse<Customer>> RemoveAsync(int id);
        public Task<ServiceResponse<Customer>> UpdateAsync(Customer customer);
        public Task<ServiceResponse<Customer>> GetByIdAsync(int id);
    }
}
