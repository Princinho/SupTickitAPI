using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Infrastructure;
using SupTickit.API.DTOs;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;
        public TicketsController(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetAll()
        {
            var tickets = await _repository.GetAllAsync();
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("/customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetByCustomerId(int customerId)
        {
            var tickets = await _repository.GetByCustomerId(customerId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("/agent/{agentId}")]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetByAgentId(int agentId)
        {
            var tickets = await _repository.GetByAgentId(agentId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("/moderator/{moderatorId}")]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> getByModeratorId(int moderatorId)
        {
            var tickets = await _repository.GetByModeratorId(moderatorId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Create(TicketCreateDTO ticketDto)
        {
           var dbTicket= await _repository.CreateAsync(_mapper.Map<Ticket>(ticketDto));
            return CreatedAtAction(nameof(Create), new { id = dbTicket.Id }, _mapper.Map<TicketGetAllDTO>(dbTicket));
        }
        [HttpPut]
        [Route("{ticketId}/assignTo/{userId}")]
        public async Task<ActionResult> Assign(int ticketId, int userId)
        {
            await _repository.AssignTicketAsync(ticketId, userId);
            return NoContent();
        }
        [HttpPut]
        public async Task<ActionResult> Update(TicketUpdateDTO ticketDto, int id)
        {
            await _repository.UpdateAsync(_mapper.Map<Ticket>(ticketDto),id);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<Company>> Delete(int id)
        {
            var ticket = await _repository.DeleteAsync(id);
            return Ok(ticket);
        }
    }
}
