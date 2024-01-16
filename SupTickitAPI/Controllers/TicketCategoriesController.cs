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
        public ActionResult<IEnumerable<TicketCategory>> GetAll()
        {
            return Ok(_ticketCategoryRepository.GetAll());
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<TicketCategory> GetCategoryById(int id)
        {
            return _ticketCategoryRepository.GetCategoryById(id);
        }
        [HttpGet("project/{id}")]
        public async Task<ActionResult<IEnumerable<TicketCategory>>> GetCategoriesByProjectAsync([FromQuery] int projectId)
        {
            var categories= await _ticketCategoryRepository.GetByProjectIdAsync(projectId);
            return Ok(categories);
        }

        [HttpPost]
        public ActionResult<IEnumerable<TicketCategory>> CreateCategory(TicketCategoryInputDTO category)
        {
            try
            {
                var newCategory = _mapper.Map<TicketCategory>(category);
                _ticketCategoryRepository.CreateCategory(newCategory);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok();
        }
        [HttpPut]
        public ActionResult EditCategory(TicketCategory category, int id)
        {
            _ticketCategoryRepository.UpdateCategory(category, id);
            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteCategory( int id)
        {
            _ticketCategoryRepository.DeleteCategory( id);
            return Ok();
        }
    }
}
