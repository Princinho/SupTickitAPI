using Suptickit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IQuoteRepository
    {
        public Task<ServiceResponse<IEnumerable<Quote>>> GetAllAsync();
        public Task<ServiceResponse<Quote>> AddAsync(Quote quote);
        public Task<ServiceResponse<Quote>> RemoveAsync(int id);
        public Task<ServiceResponse<Quote>> UpdateAsync(Quote quote);
        public Task<ServiceResponse<Quote>> GetByIdAsync(int id);
    }
}
