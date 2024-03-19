using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.Domain;
using System.Net.Http.Headers;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;
        public AttachmentsController(IAttachmentRepository repository, IMapper mapper)
        {
            _attachmentRepository = repository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Attachment>> GetAll()
        {
            return Ok(_attachmentRepository.GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Attachment>> Delete(int id)
        {
            var result = await _attachmentRepository.DeleteAttachment(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("byTicket/{ticketId}")]
        public ActionResult<IEnumerable<Attachment>> GetByTicketId(int ticketId)
        {
            return Ok(_attachmentRepository.GetByTicketId(ticketId));
        }

        [HttpPost("UploadFile/{ticketId}")]
        public IActionResult Upload(int ticketId)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileExtSeparatorIndex = fileName.LastIndexOf('.');
                    var storingFileName = fileName.Insert(fileExtSeparatorIndex, DateTime.UtcNow.Ticks.ToString());
                    var fullPath = Path.Combine(pathToSave, storingFileName);
                    var dbPath = Path.Combine(folderName, storingFileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    _attachmentRepository.CreateAttachment(new Attachment { DateCreated = DateTime.Now, FileName = fileName, FilePath = dbPath, TicketId = ticketId });
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
