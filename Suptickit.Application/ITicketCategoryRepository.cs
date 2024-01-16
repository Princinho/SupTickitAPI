using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ITicketCategoryRepository
    {
        IEnumerable<TicketCategory> GetAll();
        Task<IEnumerable<TicketCategory>> GetByProjectIdAsync(int id);
        TicketCategory GetCategoryById(int id);
        void CreateCategory(TicketCategory category);
        void UpdateCategory(TicketCategory category,int id);
        TicketCategory DeleteCategory(int id);
    }
}
