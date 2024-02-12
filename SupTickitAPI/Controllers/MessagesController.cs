using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.Domain;
using Suptickit.Infrastructure;
using SupTickit.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using SupTickit.API.CustomAttributes;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;
        public MessagesController(IMessageRepository messagesRepository, IMapper mapper)
        {
            _repository = messagesRepository;
            _mapper = mapper;
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetById(int id)
        {
            return Ok(await _repository.GetByIdAsync(id));
        }
        [HttpGet("/api/Tickets/{id}/messages")]
        public async Task<ActionResult<IEnumerable<MessageGetAllDTO>>> GetByTicketIdAsync(int id)
        {
            var messages=_mapper.Map<IEnumerable<MessageGetAllDTO>>(await _repository.GetByTicketId(id));
            return Ok(messages);
        }
        [HttpPost]
        public async Task<ActionResult> AddAsync(MessageCreateDTO messageDTO)
        {
            var dbMessage = _mapper.Map<Message>(messageDTO);
            await _repository.CreateAsync(dbMessage);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(MessageUpdateDTO messageDTO, int id)
        {
            var dbMessage = _mapper.Map<Message>(messageDTO);
            await _repository.UpdateAsync(dbMessage,  id);
            return NoContent();
        }
        [HttpDelete]
        [AdminLevel]
        public async Task<ActionResult> DeleteAsync( int id)
        {
            var message= await _repository.DeleteAsync(id);
            return Ok(message);
        }
    }
}
