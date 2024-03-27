using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Domain.Models;
using SupTickit.API.DTOs;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartCategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPartCategoryRepository _repo;
        public PartCategoriesController( IMapper mapper, IPartCategoryRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PartCategory>>> GetAll()
        {
            var response = await _repo.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PartCategory>> GetById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PartCategory>>> Create(PartCategoryCreateDTO categoryDTO)
        {
            var response = await _repo.AddAsync(_mapper.Map<PartCategory>(categoryDTO));
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PartCategory>> Update(PartCategoryEditDTO category)
        {
            var response = await _repo.UpdateAsync(_mapper.Map<PartCategory>(category));
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PartCategory>> Delete(int id)
        {
            var response = await _repo.RemoveAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Message);
        }
    }
}
