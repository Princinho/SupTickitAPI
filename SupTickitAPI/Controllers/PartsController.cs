using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Domain.Models;
using SupTickit.API.DTOs;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPartRepository _repo;
        public PartsController(IMapper mapper, IPartRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartGetAllDTO>>> GetAll()
        {
            var response = await _repo.GetAllAsync();
            if (response.Success)
            {
                return Ok(_mapper.Map<IEnumerable<PartGetAllDTO>>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Part>> GetById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<PartGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<ActionResult<PartGetAllDTO>> Create(PartCreateDTO serviceDTO)
        {
            var response = await _repo.AddAsync(_mapper.Map<Part>(serviceDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<PartGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PartGetAllDTO>> Update(PartUpdateDTO serviceDTO)
        {
            var response = await _repo.UpdateAsync(_mapper.Map<Part>(serviceDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<PartGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PartGetAllDTO>> Delete(int id)
        {
            var response = await _repo.RemoveAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<PartGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
    }
}
