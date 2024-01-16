using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task<Company> CreateAsync(Company company);
        Task UpdateAsync(Company company, int id);
        Task<Company> DeleteAsync(int id);
    }
}
