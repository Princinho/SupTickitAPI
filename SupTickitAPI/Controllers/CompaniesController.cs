using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.API.CustomAttributes;
using SupTickit.API.DTOs;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<CompanyGetAllDTO>>> GetAllAsync()
        {
            var dbCompanies= await _companyRepository.GetAllAsync();
            var companyDTOs=_mapper.Map<IEnumerable<CompanyGetAllDTO>>(dbCompanies);
            return Ok(dbCompanies);
        }

        [HttpPost]
        [AdminLevel]
        public async Task<ActionResult> Create(CompanyCreateDTO company)
        {
            var dbCompany=await _companyRepository.CreateAsync(_mapper.Map<Company>(company));
            return CreatedAtAction(nameof(Create), new { id = dbCompany.Id }, dbCompany);
        }
        [HttpPut("{id}")]
        [AdminLevel]
        public async Task<ActionResult> Update(CompanyUpdateDTO company, int id)
        {
            
            await _companyRepository.UpdateAsync(_mapper.Map<Company>(company), id);
            return Ok();
        }
        [HttpDelete("{id}")]
        [AdminLevel]
        public async Task<ActionResult<Company>> Delete(int id)
        {
            var company = await _companyRepository.DeleteAsync(id);
            return Ok(company);
        }
    }
}
