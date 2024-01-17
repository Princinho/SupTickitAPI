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
    public class TicketRepository : ITicketRepository
    {
        private readonly SuptickitContext _context;
        public TicketRepository(SuptickitContext context)
        {
            _context = context;
        }

        public async Task AssignTicketAsync(int ticketId, int userId)
        {
            var ticket=await GetbyIdAsync(ticketId);
            ticket.AgentId= userId;
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> DeleteAsync(int id)
        {
            var ticket = _context.Tickets.Find(id);
            _context.Tickets.Remove(ticket);
               await _context.SaveChangesAsync();
            
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByAgentId(int id)
        {
            return await _context.Tickets.Where(t=>t.AgentId==id).ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByCustomerId(int id)
        {
            return await _context.Tickets.Where(t => t.CreatedBy == id).ToListAsync();
        }

        public async Task<Ticket> GetbyIdAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<IEnumerable<Ticket>> GetByModeratorId(int id)
        {
            return await _context.Tickets.Where(t => t.AssignedBy == id).ToListAsync();
        }

        public async Task UpdateAsync(Ticket ticket, int id)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
    }
}
