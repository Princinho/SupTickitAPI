using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
