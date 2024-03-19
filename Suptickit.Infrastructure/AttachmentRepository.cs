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
        public AttachmentRepository(SuptickitContext context)
        {
            _context = context;
        }
        public void CreateAttachment(Attachment attachment)
        {
            attachment.DateCreated = DateTime.UtcNow;
            _context.Attachments.Add(attachment);
            _context.SaveChanges();
        }

        public async Task<ServiceResponse<Attachment>> DeleteAttachment(int id)
        {
            var attachment = _context.Attachments.FirstOrDefault(x => x.Id == id);
            try
            {
                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Attachment> { Data = attachment, Success = true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Attachment> { Data = attachment, Success = false, Message = "Failed to delete attachment, " + ex.Message };
            }
        }

        public IEnumerable<Attachment> GetAll()
        {
            return _context.Attachments.ToList();
        }

        public Attachment GetAttachment(int id)
        {
            return _context.Attachments.Find(id);
        }

        public IEnumerable<Attachment> GetByTicketId(int id)
        {
            return _context.Attachments.Where(attachment => attachment.TicketId == id).ToList();
        }

        public void UpdateAttachment(Attachment Attachment, int id)
        {
            if (Attachment.Id != id) throw new ArgumentException("ids do not match for update");
            _context.Attachments.Update(Attachment);
            _context.SaveChanges();
        }
        public async Task PostFileAsync(Attachment attachment)
        {
            var result = _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
        }

    }
}
