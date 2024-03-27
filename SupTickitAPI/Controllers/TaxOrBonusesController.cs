using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupTickit.API.DTOs;
using Suptickit.Application;
using Suptickit.Domain.Models;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxOrBonusesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITaxOrBonusRepository _repo;
        public TaxOrBonusesController(IMapper mapper, ITaxOrBonusRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet("{inclArchived}")]
        public async Task<ActionResult<IEnumerable<TaxOrBonusGetAllDTO>>> GetAll(bool inclArchived)
        {
            var response = inclArchived ? await _repo.GetAllWithArchivedAsync():await _repo.GetAllAsync() ;
            if (response.Success)
            {
                return Ok(_mapper.Map<IEnumerable<TaxOrBonusGetAllDTO>>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<TaxOrBonusGetAllDTO>> GetById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<TaxOrBonusGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<ActionResult<TaxOrBonusGetAllDTO>> Create(TaxOrBonusCreateDTO data)
        {
            var response = await _repo.AddAsync(_mapper.Map<TaxOrBonus>(data));
            if (response.Success)
            {
                return Ok(_mapper.Map<TaxOrBonusGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TaxOrBonusGetAllDTO>> Update(TaxOrBonusUpdateDTO data)
        {
            var response = await _repo.UpdateAsync(_mapper.Map<TaxOrBonus>(data));
            if (response.Success)
            {
                return Ok(_mapper.Map<TaxOrBonusGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaxOrBonusGetAllDTO>> Delete(int id)
        {
            var response = await _repo.RemoveAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<TaxOrBonusGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
    }
}
