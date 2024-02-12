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
    public class TicketLogRepository : ITicketLogRepository
    {
        private readonly SuptickitContext _context;
        public TicketLogRepository(SuptickitContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TicketLog>> getByTicketId(int ticketId)
        {
            return await _context.TicketLogs.Where(t => t.TicketId == ticketId).ToListAsync();
        }
    }
}
