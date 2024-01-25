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
    public class MessageRepository : IMessageRepository
    {
        private readonly SuptickitContext _context;
        public MessageRepository(SuptickitContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Message Message)
        {
            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> DeleteAsync(int id)
        {
            var message= _context.Messages.FirstOrDefault(x => x.Id == id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetByTicketId(int id)
        {
            return await _context.Messages.Where(message=>message.TicketId==id).ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync();            
        }

        public async Task UpdateAsync(Message Message, int id)
        {
            if(Message.Id != id) { throw new ArgumentException("Ids do not match for message update"); }
            _context.Messages.Update(Message);
            await _context.SaveChangesAsync();
        }
    }
}
