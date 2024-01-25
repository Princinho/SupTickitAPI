using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<IEnumerable<Message>> GetByTicketId(int id);
        Task<Message> GetByIdAsync(int id);
        Task CreateAsync(Message Message);
        Task UpdateAsync(Message Message, int id);
        Task<Message> DeleteAsync(int id);
    }
}
