using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IMessageMessage
    {
        IEnumerable<Message> GetAll();
        Message GetMessage(int id);
        void CreateMessage(Message Message);
        void UpdateMessage(Message Message, int id);
        Message DeleteMessage(int id);
    }
}
