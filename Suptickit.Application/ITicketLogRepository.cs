using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface ITicketLogRepository
    {
        public Task<IEnumerable<TicketLog>> getByTicketId(int  ticketId);
    }
}
