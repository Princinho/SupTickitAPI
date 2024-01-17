using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket> GetbyIdAsync(int id);
        Task<IEnumerable<Ticket>> GetByCustomerId(int id);
        Task<IEnumerable<Ticket>> GetByAgentId(int id);
        Task<IEnumerable<Ticket>> GetByModeratorId(int id);
        Task<Ticket> CreateAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket, int id);
        Task<Ticket> DeleteAsync(int id);
        Task AssignTicketAsync(int ticketId, int userId);
    }
}
