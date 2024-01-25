using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Infrastructure;
using SupTickit.API.CustomAttributes;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository _repository;
        private readonly ITicketCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProjectsController(
            IProjectsRepository repository,
            IMapper mapper,
            ITicketCategoryRepository categoryRipository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRipository;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {
            var projects = await _repository.GetAll();
            var projectsOutputDTO = _mapper.Map<IEnumerable<ProjectGetAllDTO>>(projects);
            return Ok(projectsOutputDTO);
        }

        [HttpPost]
        [AdminLevel]
        public async Task<ActionResult<Project>> Create(ProjectInputDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            var dbProject = await _repository.AddAsync(project);
            var projectOutputDTO = _mapper.Map<ProjectGetAllDTO>(dbProject);
            return CreatedAtAction(nameof(Create), new { id = dbProject.Id }, projectOutputDTO);
        }
        [HttpPost("CreateWithCompanies")]
        [AdminLevel]
        public async Task<ActionResult<Project>> CreateAndAssignToCompanies(ProjectWithCompaniesInputDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            var dbProject = await _repository.AddAsync(project);
            foreach (var companyId in projectDTO.CompanyIds)
            {
                //Assign to companies
                await _repository.AssignToCompanyAsync(dbProject.Id, companyId);
            }
            var projectOutputDTO = _mapper.Map<ProjectGetAllDTO>(dbProject);
            return CreatedAtAction(nameof(Create), new { id = dbProject.Id }, projectOutputDTO);
        }
        [HttpPatch]
        [AdminLevel]
        [Route("{projectId}/assignCompany/{companyId}")]
        public async Task<ActionResult> assignToCompany(int projectId, int companyId)
        {
            await _repository.AssignToCompanyAsync(projectId, companyId);
            return Ok();
        }
        [HttpPut]
        [AdminLevel]
        public async Task<ActionResult> Edit(ProjectEditDTO project, int id)
        {
            await _repository.UpdateProjectAsync(_mapper.Map<Project>(project), id);
            return Ok();
        }
        [HttpDelete]
        [AdminLevel]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
