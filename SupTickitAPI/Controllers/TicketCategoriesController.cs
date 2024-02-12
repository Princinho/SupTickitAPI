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
    public class TicketCategoriesController : ControllerBase
    {
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;
        public TicketCategoriesController(ITicketCategoryRepository repository, IMapper mapper)
        {
            _ticketCategoryRepository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TicketCategoryOutDTO>> GetAll()
        {
            return Ok(_mapper.Map<IEnumerable< TicketCategoryOutDTO>>(_ticketCategoryRepository.GetAll()));
        }
        [HttpGet]
        [Route("{id}")]

        public ActionResult<TicketCategoryOutDTO> GetCategoryById(int id)
        {
            return Ok(_mapper.Map<TicketCategoryOutDTO>(_ticketCategoryRepository.GetCategoryById(id)));
        }
        [HttpGet("project/{id}")]
        public async Task<ActionResult<IEnumerable<TicketCategory>>> GetCategoriesByProjectAsync([FromQuery] int projectId)
        {
            var categories = await _ticketCategoryRepository.GetByProjectIdAsync(projectId);
            return Ok(categories);
        }

        [HttpPost]
        [ModsLevel]
        public ActionResult CreateCategory(TicketCategoryInputDTO category)
        {
            try
            {
                var newCategory = _mapper.Map<TicketCategory>(category);
                newCategory.CreatedBy = int.Parse(User.Claims.First(c => c.Type == "id").Value);
                _ticketCategoryRepository.CreateCategory(newCategory);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpPut("{id}")]
        [ModsLevel]
        public ActionResult EditCategory([FromBody] TicketCategoryEditDTO category, int id)
        {
            _ticketCategoryRepository.UpdateCategory(_mapper.Map<TicketCategory>(category), id);
            return Ok();
        }
        [HttpDelete("{id}")]
        [ModsLevel]
        public ActionResult DeleteCategory(int id)
        {
            _ticketCategoryRepository.DeleteCategory(id);
            return Ok();
        }
    }
}
