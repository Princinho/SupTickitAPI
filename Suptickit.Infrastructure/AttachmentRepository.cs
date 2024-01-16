using Suptickit.Application;
using SupTickit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suptickit.Infrastructure
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly SuptickitContext _context;
        public AttachmentRepository(SuptickitContext context) { 
            _context= context;
        }
        public void CreateAttachment(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            _context.SaveChanges();
        }

        public Attachment DeleteAttachment(int id)
        {
            var attachment = _context.Attachments.FirstOrDefault(x => x.Id == id);
            _context.Attachments.Remove(attachment);
            _context.SaveChanges();
            return attachment;
        }

        public IEnumerable<Attachment> GetAll()
        {
            return _context.Attachments.ToList();
        }

        public Attachment GetAttachment(int id)
        {
            return _context.Attachments.Find(id);
        }

        public void UpdateAttachment(Attachment Attachment, int id)
        {
            if (Attachment.Id != id) throw new ArgumentException("ids do not match for update");
            _context.Attachments.Update(Attachment);
            _context.SaveChanges();
        }
    }
}
