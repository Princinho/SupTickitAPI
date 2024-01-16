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
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly SuptickitContext _context;
        public TicketCategoryRepository(SuptickitContext context)
        {
            _context = context;
        }
        public void CreateCategory(TicketCategory category)
        {
            _context.TicketCategories.Add(category);
            _context.SaveChanges();
        }

        public TicketCategory DeleteCategory(int id)
        {
            var category = _context.TicketCategories.Find(id) ?? throw new InvalidOperationException($"Entry with id {id} does not exist");
            _context.TicketCategories.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public IEnumerable<TicketCategory> GetAll()
        {
            return _context.TicketCategories.ToList();
        }

        public TicketCategory GetCategoryById(int id)
        {
            return _context.TicketCategories.Find(id);
        }

        public void UpdateCategory(TicketCategory category, int id)
        {
            if(category.Id!=id)
            {
                throw new ArgumentException("Conflicting arguments for update, ids do not match");
            }
            _context.TicketCategories.Update(category);
            _context.SaveChanges();
        }

        public IEnumerable<TicketCategory> getCategoriesByProjectId(int projectId)
        {
            return _context.TicketCategories.Where(category => category.ProjectId == projectId).ToList();
        }

        public async Task<IEnumerable<TicketCategory>> GetByProjectIdAsync(int id)
        {
            return await _context.TicketCategories.Where(category => category.ProjectId == id).ToListAsync();
        }
    }
}
