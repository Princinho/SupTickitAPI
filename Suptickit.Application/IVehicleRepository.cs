using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IVehicleRepository
    {
        public Task<ServiceResponse<IEnumerable<Vehicle>>> GetAllAsync();
        public Task<ServiceResponse<Vehicle>> AddAsync(Vehicle vehicle);
        public Task<ServiceResponse<Vehicle>> RemoveAsync(int id);
        public Task<ServiceResponse<Vehicle>> UpdateAsync(Vehicle vehicle);
        public Task<ServiceResponse<Vehicle>> GetByIdAsync(int id);
    }
}
