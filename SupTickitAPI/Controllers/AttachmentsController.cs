using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Domain.Enums;
using Suptickit.Infrastructure;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

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
        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] AttachmentInputDTO attachmentDTO)
        {
            if (attachmentDTO == null)
            {
                return BadRequest();
            }

            try
            {
                await PostFileAsync(attachmentDTO.FileDetails, attachmentDTO.FileType,attachmentDTO.TicketId);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Attachment>> GetAll()
        {
            return Ok(_attachmentRepository.GetAll());
        }
        [HttpPost("AttachmentFile")]
        public async Task PostFileAsync(IFormFile fileData, FileType fileType,int ticketId)
        {
            try
            {
                var fileDetails = new Attachment()
                {
                    Id = 0,
                    FileName = fileData.FileName,
                    FileType = fileType,
                    TicketId= ticketId
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }
                await _attachmentRepository.PostFileAsync(fileDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("AttachmentFile")]
        public async Task<IActionResult> DownloadFileById(int id)
        {
            try
            {
                var file = _attachmentRepository.GetAttachment(id);

                var content = new MemoryStream(file.FileData);
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "FileDownloaded",
                   file.FileName);



                await  Utils.CopyStream(content, path);
                byte[] fileBytes=System.IO.File.ReadAllBytes(path);
                return File(fileBytes, "application/force-download", "file.jpg");
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        [HttpPost]
        public ActionResult<IEnumerable<Attachment>> Create(AttachmentInputDTO entry)
        {
            try
            {
                var newEntry = _mapper.Map<Attachment>(entry);
                _attachmentRepository.CreateAttachment(newEntry);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
    }
}
