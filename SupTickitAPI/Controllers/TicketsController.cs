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
        private readonly ITicketLogRepository _ticketLogsRepository;
        private readonly IUsersRepository _usersRepo;
        private readonly IMessageRepository _messageRepo;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly IMapper _mapper;
        public TicketsController(ITicketRepository repository, IUsersRepository usersRepo, IAttachmentRepository attachmentRepo, IMessageRepository messaeRepo, IMapper mapper)
        {
            _repository = repository;
            _usersRepo = usersRepo;
            _mapper = mapper;
            _attachmentRepo = attachmentRepo;
            _messageRepo = messaeRepo;
        }
        [HttpGet]
        [AdminLevel]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetAll()
        {
            var tickets = await _repository.GetAllAsync();
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetById(int id)
        {
            var ticket = await _repository.GetbyIdAsync(id);
            var ticketDTO = _mapper.Map<TicketGetAllDTO>(ticket);
            return Ok(ticketDTO);
        }
        [HttpGet("{id}/logs")]
        [CustomersOnly]
        public async Task<ActionResult<IEnumerable<TicketLogDTO>>> GetLogs(int id)
        {
            var ticketlogs = await _ticketLogsRepository.getByTicketId(id);
            var attachments = _attachmentRepo.GetByTicketId(id);
            var messages = await _messageRepo.GetByTicketId(id);
            var logs = new List<TicketLogDTO>();
            foreach (var log in ticketlogs)
            {
                var logDTO = _mapper.Map<TicketLogDTO>(log);
                logs.Add(logDTO);
            }
            foreach (var attachment in attachments)
            {
                logs.Add(new TicketLogDTO { LogDate = attachment.DateCreated, AttachmentId = attachment.Id});
            }
            foreach (var message in messages)
            {
                logs.Add(new TicketLogDTO { LogDate = message.DateCreated, MessageBody = message.Body, MessageId = message.Id });
            }

            return Ok(logs.OrderByDescending(log=>log.DateCreated));
        }
        [HttpGet("customer/{customerId}")]
        [CustomersOnly]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetByCustomerId(int customerId)
        {
            var tickets = await _repository.GetByCustomerId(customerId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        [HttpGet("agent/{agentId}")]
        [AgentLevel]
        public async Task<ActionResult<IEnumerable<TicketGetAllDTO>>> GetByAgentId(int agentId)
        {
            var tickets = await _repository.GetByAgentId(agentId);
            var ticketsDTO = _mapper.Map<IEnumerable<TicketGetAllDTO>>(tickets);
            return Ok(ticketsDTO);
        }
        
        [HttpPost]
        [CustomersOnly]
        public async Task<ActionResult> Create(TicketCreateDTO ticketDto)
        {
            var newTicket = _mapper.Map<Ticket>(ticketDto);
            newTicket.CreatedBy = int.Parse(User.Claims.First(c => c.Type == "id").Value);
            var dbTicket = await _repository.CreateAsync(newTicket);
            return CreatedAtAction(nameof(Create), new { id = dbTicket.Id }, _mapper.Map<TicketGetAllDTO>(dbTicket));
        }
        [HttpPut]
        [AgentLevel]
        [Route("{ticketId}/assignTo/{userId}")]
        public async Task<ActionResult> Assign(int ticketId, int userId)
        {
            var moderatorId = int.Parse(User.Claims.First(c => c.Type == "id").Value);
            await _repository.AssignTicketAsync(ticketId, userId,moderatorId);
            return NoContent();
        }
        [HttpPut("{id}")]
        [AgentLevel]
        public async Task<ActionResult> Update(TicketUpdateDTO ticketDto, int id)
        {
            /*var dbUser = await _usersRepo.GetByName(Utils.getUsername(User));
            var dbTicket = await _repository.GetbyIdAsync(id);
            if (dbUser.Id != dbTicket.CreatedBy && dbUser.Id != dbTicket.AgentId)
            {
                return BadRequest("You are not the owner or the agent of this ticket");
            }*/
            await _repository.UpdateAsync(_mapper.Map<Ticket>(ticketDto), id);
            return NoContent();
        }
        [HttpPut("StartProcessing/{id}")]
        [AgentLevel]
        public async Task<ActionResult> StartProcessing(int id)
        {
            var dbUser = await _usersRepo.GetByName(User.Identity.Name);
            var dbTicket = await _repository.GetbyIdAsync(id);
            if (dbUser.Id != dbTicket.AgentId) return BadRequest("You are not the agent assigned to this ticket");
            dbTicket.Status = TicketStatusEnum.Processing;
            await _repository.UpdateAsync(dbTicket, id);
            return NoContent();
        }
        [HttpPut("EndProcessing/{id}")]
        [AgentLevel]
        public async Task<ActionResult> EndProcessing(int id)
        {
            var dbUser = await _usersRepo.GetByName(User.Identity.Name);
            var dbTicket = await _repository.GetbyIdAsync(id);
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
