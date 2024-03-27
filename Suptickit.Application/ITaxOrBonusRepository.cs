using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ITaxOrBonusRepository
    {
        public Task<ServiceResponse<IEnumerable<TaxOrBonus>>> GetAllAsync();
        public Task<ServiceResponse<IEnumerable<TaxOrBonus>>> GetAllWithArchivedAsync();
        public Task<ServiceResponse<TaxOrBonus>> AddAsync(TaxOrBonus taxOrBonus);
        public Task<ServiceResponse<TaxOrBonus>> RemoveAsync(int id);
        public Task<ServiceResponse<TaxOrBonus>> UpdateAsync(TaxOrBonus taxOrBonus);
        public Task<ServiceResponse<TaxOrBonus>> GetByIdAsync(int id);
    }
}
