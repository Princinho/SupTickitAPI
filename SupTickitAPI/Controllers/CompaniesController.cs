using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAllAsync()
        {
            return Ok(await _companyRepository.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CompanyCreateDTO company)
        {
            var dbCompany=await _companyRepository.CreateAsync(_mapper.Map<Company>(company));
            return CreatedAtAction(nameof(Create), new { id = dbCompany.Id }, dbCompany);
        }
        [HttpPut]
        public async Task<ActionResult> Update(CompanyUpdateDTO company, int id)
        {
            
            await _companyRepository.UpdateAsync(_mapper.Map<Company>(company), id);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult<Company>> Delete(int id)
        {
            var company = await _companyRepository.DeleteAsync(id);
            return Ok(company);
        }
    }
}
