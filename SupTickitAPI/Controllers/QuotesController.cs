using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupTickit.API.DTOs;
using Suptickit.Application;
using Suptickit.Domain.Models;
using Suptickit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IQuoteRepository _repo;
        SuptickitContext _context;
        public QuotesController(IMapper mapper, IQuoteRepository repo, SuptickitContext context)
        {
            _mapper = mapper;
            _repo = repo;
            _context = context;
        }
        [HttpGet("nextref")]
        public async Task<string> GetNextRef()
        {
            var today = DateTime.Now.Date;
            var lastQuote = await _context.Quotes.Where(q => q.DateCreated.Date.Month == today.Month && q.DateCreated.Year == today.Year)
                .OrderByDescending(q => q.Id).FirstOrDefaultAsync();
            if (lastQuote != null)
            {
                var lastRef = lastQuote.ReferenceNumber;
                var lastRefNum = lastRef[..4];
                var newRefNum = int.Parse(lastRefNum) + 1;
                var newRef = newRefNum + "/" + (today.Month > 9 ? today.Month : "0" + today.Month) + "/" + today.Year % 1000;
                newRef = Utils.FillToLength(newRef, 10, "0");
                newRef += "CF";
                return newRef;
            }
            else
            {
                var newRef = "1" + "/" + (today.Month > 9 ? today.Month : "0" + today.Month) + "/" + today.Year % 1000;
                newRef = Utils.FillToLength(newRef, 10, "0");
                newRef += "CF";
                return newRef;
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuoteGetAllDTO>>> GetAll()
        {
            var response = await _repo.GetAllAsync();
            if (response.Success)
            {
                return Ok(_mapper.Map<IEnumerable<QuoteGetAllDTO>>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<QuoteGetAllDTO>> GetById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<QuoteGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<ActionResult<QuoteGetAllDTO>> Create(QuoteCreateDTO vehicleDTO)
        {
            var response = await _repo.AddAsync(_mapper.Map<Quote>(vehicleDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<QuoteGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<QuoteGetAllDTO>> Update(QuoteUpdateDTO QuoteDTO)
        {
            var response = await _repo.UpdateAsync(_mapper.Map<Quote>(QuoteDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<QuoteGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuoteGetAllDTO>> Delete(int id)
        {
            var response = await _repo.RemoveAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<QuoteGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
    }
}
