using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Infrastructure;
using SupTickit.API.CustomAttributes;
using SupTickit.API.DTOs;
using SupTickit.Domain;
using SupTickitAPI.DTOs;
using SupTickitAPI.Enums;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketRepository _repository;
        private readonly IUsersRepository _usersRepo;
        private readonly IMapper _mapper;
        public TicketsController(ITicketRepository repository,IUsersRepository usersRepo, IMapper mapper)
        {
            _repository = repository;
            _usersRepo = usersRepo;
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
        [CustomersOnly]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetByCustomerId(int customerId)
        {
            var tickets = await _repository.GetByCustomerId(customerId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("/agent/{agentId}")]
        [AgentLevel]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetByAgentId(int agentId)
        {
            var tickets = await _repository.GetByAgentId(agentId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("/moderator/{moderatorId}")]
        [ModsLevel]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> getByModeratorId(int moderatorId)
        {
            var tickets = await _repository.GetByModeratorId(moderatorId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpPost]
        [CustomersOnly]
        public async Task<ActionResult> Create(TicketCreateDTO ticketDto)
        {
           var dbTicket= await _repository.CreateAsync(_mapper.Map<Ticket>(ticketDto));
            return CreatedAtAction(nameof(Create), new { id = dbTicket.Id }, _mapper.Map<TicketGetAllDTO>(dbTicket));
        }
        [HttpPut]
        [ModsLevel]
        [Route("{ticketId}/assignTo/{userId}")]
        public async Task<ActionResult> Assign(int ticketId, int userId)
        {
            await _repository.AssignTicketAsync(ticketId, userId);
            return NoContent();
        }
        [HttpPut]
        [AgentLevel]
        public async Task<ActionResult> Update(TicketUpdateDTO ticketDto, int id)
        {
            var dbUser = await _usersRepo.GetByName(User.Identity.Name);
            var dbTicket=await _repository.GetbyIdAsync(id);
            if (dbUser.Id != dbTicket.CreatedBy && dbUser.Id!=dbTicket.AgentId)
            {
                return BadRequest("You are not the owner or the agent of this ticket");
            }
            await _repository.UpdateAsync(_mapper.Map<Ticket>(ticketDto),id);
            return NoContent();
        }
        [HttpPut("StartProcessing")]
        [AgentLevel]
        public async Task<ActionResult> StartProcessing(int id)
        {
            var dbUser = await _usersRepo.GetByName(User.Identity.Name);
            var dbTicket=await _repository.GetbyIdAsync(id);
            if (dbUser.Id != dbTicket.AgentId) return BadRequest("You are not the agent assigned to this ticket");
            dbTicket.Status = TicketStatusEnum.Processing;
            await _repository.UpdateAsync(dbTicket, id);
            return NoContent();
        }
        [HttpPut("EndProcessing")]
        [AgentLevel]
        public async Task<ActionResult> EndProcessing(int id)
        {
            var dbUser = await _usersRepo.GetByName(User.Identity.Name);
            var dbTicket=await _repository.GetbyIdAsync(id);
            if (dbUser.Id != dbTicket.AgentId) return BadRequest("You are not the agent assigned to this ticket");
            dbTicket.Status = TicketStatusEnum.Processed;
            await _repository.UpdateAsync(dbTicket, id);
            return NoContent();
        }
        [HttpDelete]
        [ModsLevel]
        public async Task<ActionResult<Company>> Delete(int id)
        {
            var ticket = await _repository.DeleteAsync(id);
            return Ok(ticket);
        }
    }
}
