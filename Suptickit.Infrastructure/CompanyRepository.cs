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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly SuptickitContext _context;
        public CompanyRepository(SuptickitContext context)
        {
            _context = context;
        }
        public async Task<Company> CreateAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company> DeleteAsync(int id)
        {
            var company = _context.Companies.Find(id);
            if(company == null) { throw new ArgumentException("Company with id "+id+" does not exist"+nameof(company)); }
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if(company == null) { throw new ArgumentException("Company with id " + id + " does not exist" + nameof(company)); }
            return company;
        }

        public async Task UpdateAsync(Company company, int id)
        {
            if(company.Id!=id ) { throw new ArgumentException("Ids do not match for update"); }
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
        }
    }
}
