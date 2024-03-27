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
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _repo;
        public VehiclesController(IMapper mapper, IVehicleRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleGetAllDTO>>> GetAll()
        {
            var response = await _repo.GetAllAsync();
            if (response.Success)
            {
                return Ok(_mapper.Map<IEnumerable<VehicleGetAllDTO>>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleGetAllDTO>> GetById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<VehicleGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<ActionResult<VehicleGetAllDTO>> Create(VehicleCreateDTO vehicleDTO)
        {
            var response = await _repo.AddAsync(_mapper.Map<Vehicle>(vehicleDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<VehicleGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleGetAllDTO>> Update(VehicleUpdateDTO VehicleDTO)
        {
            var response = await _repo.UpdateAsync(_mapper.Map<Vehicle>(VehicleDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<VehicleGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleGetAllDTO>> Delete(int id)
        {
            var response = await _repo.RemoveAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<VehicleGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
    }
}
