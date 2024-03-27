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
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _repo;
        public CustomersController(IMapper mapper, ICustomerRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGetAllDTO>>> GetAll()
        {
            var response = await _repo.GetAllAsync();
            if (response.Success)
            {
                return Ok(_mapper.Map<IEnumerable<CustomerGetAllDTO>>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerGetAllDTO>> GetById(int id)
        {
            var response = await _repo.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<CustomerGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerGetAllDTO>> Create(CustomerCreateDTO customerDTO)
        {
            var response = await _repo.AddAsync(_mapper.Map<Customer>(customerDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<CustomerGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerGetAllDTO>> Update(CustomerUpdateDTO customerDTO)
        {
            var response = await _repo.UpdateAsync(_mapper.Map<Customer>(customerDTO));
            if (response.Success)
            {
                return Ok(_mapper.Map<CustomerGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerGetAllDTO>> Delete(int id)
        {
            var response = await _repo.RemoveAsync(id);
            if (response.Success)
            {
                return Ok(_mapper.Map<CustomerGetAllDTO>(response.Data));
            }
            return BadRequest(response.Message);
        }
    }
}
