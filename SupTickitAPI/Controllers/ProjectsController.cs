using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.API.CustomAttributes;
using SupTickit.Domain;
using SupTickitAPI.DTOs;

namespace SupTickitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository _repo;
        private readonly IUsersRepository _usersRepo;
        private readonly ITicketCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProjectsController(
            IProjectsRepository repository,
            IUsersRepository usersRepo,
            IMapper mapper,
            ITicketCategoryRepository categoryRipository)
        {
            _repo = repository;
            _mapper = mapper;
            _categoryRepository = categoryRipository;
            _usersRepo = usersRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {
            var projects = await _repo.GetAll();
            var projectsOutputDTO = _mapper.Map<IEnumerable<ProjectGetAllDTO>>(projects);
            return Ok(projectsOutputDTO);
        }
        [HttpGet("/byCompany/{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Project>>> GetByCompanyId(int id)
        {
            var companyProjects = await _repo.GetByCompanyId(id);
            var companyProjectsDTOs = _mapper.Map<IEnumerable<ProjectGetAllDTO>>(companyProjects);
            return Ok(companyProjectsDTOs);
        }

        //[HttpPost]
        ////[AdminLevel]
        //public async Task<ActionResult<Project>> Create(ProjectInputDTO projectDTO)
        //{
        //    var project = _mapper.Map<Project>(projectDTO);
        //    var dbProject = await _repository.AddAsync(project);
        //    var projectOutputDTO = _mapper.Map<ProjectGetAllDTO>(dbProject);
        //    return CreatedAtAction(nameof(Create), new { id = dbProject.Id }, projectOutputDTO);
        //}
        /*TODO: Empecher le meme username pour differents users*/
        [HttpPost]
        [AdminLevel]
        public async Task<ActionResult<Project>> Create(ProjectWithCompaniesInputDTO projectDTO)
        {
            var username = HttpContext.User.Claims.First(c => c.Type == "username").Value;
            var userData = await _usersRepo.GetByUserNameAsync(username);

            var project = _mapper.Map<Project>(projectDTO);
            var dbProject = await _repo.AddAsync(project,userData.Data.Id);
            if (projectDTO.CompanyIds != null)
            {
                foreach (var companyId in projectDTO.CompanyIds)
                {
                    //Assign to companies
                    await _repo.AssignToCompanyAsync(dbProject.Id, companyId);
                }
            }
            var projectOutputDTO = _mapper.Map<ProjectGetAllDTO>(dbProject);
            return CreatedAtAction(nameof(Create), new { id = dbProject.Id }, projectOutputDTO);
        }
        [HttpPost]
        [AdminLevel]
        [Route("{projectId}/assign/{companyId}")]
        public async Task<ActionResult> AssignToCompany(int projectId, int companyId)
        {
            await _repo.AssignToCompanyAsync(projectId, companyId);
            return Ok();
        }
        [HttpDelete]
        [AdminLevel]
        [Route("{projectId}/remove/{companyId}")]
        public async Task<ActionResult> UnassignCompany(int projectId, int companyId)
        {
            await _repo.UnassignCompanyAsync(projectId, companyId);
            return Ok();
        }
        [HttpPut("{id}")]
        [AdminLevel]
        public async Task<ActionResult> Edit(ProjectEditDTO project, int id)
        {
            await _repo.UpdateProjectAsync(_mapper.Map<Project>(project), id);
            return Ok();
        }
        [HttpDelete("{id}")]
        [AdminLevel]
        public async Task<ActionResult> Delete(int id)
        {
            await _repo.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
