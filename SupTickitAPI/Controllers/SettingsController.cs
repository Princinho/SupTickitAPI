using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.API.DTOs;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _settingsRepository;
        public SettingsController(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _settingsRepository.GetSettingsSlug();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpPost()]
        public async Task<ActionResult> Set(SettingsCreateDTO data)
        {
            var response = await _settingsRepository.SetSettingsSlug(data.Stringified);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
