using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Application
{
    public interface IAttachmentRepository
    {
        IEnumerable<Attachment> GetAll();
        Attachment GetAttachment(int id);
        void CreateAttachment(Attachment attachment);
        void UpdateAttachment(Attachment attachment, int id);
        Attachment DeleteAttachment(int id);
    }
}
