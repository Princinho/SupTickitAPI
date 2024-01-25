using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.API.CustomAttributes;
using SupTickit.API.DTOs;
using SupTickit.Domain;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepository repo, IMapper mapper, IAuthRepository authRepo)
        {
            _repo = repo;
            _authRepo = authRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [AdminLevel]
        public async Task<ActionResult<IEnumerable<UsersGetAllDTO>>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            var usersDTOs = _mapper.Map<IEnumerable<UsersGetAllDTO>>(users);
            return Ok(usersDTOs);
        }
        [HttpPost]
        [AdminLevel]
        public async Task<ActionResult> Create(UserRegisterDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _authRepo.Register(user, user.Password);
            var userOutDto = _mapper.Map<UserLoginOutDTO>(userDto);
            return Ok(userOutDto);
        }
        [HttpPost("AddWithRoles")]
        [AdminLevel]
        public async Task<ActionResult> CreateWithRoles(UserWithRolesCreateDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _authRepo.Register(user, user.Password);
            return Ok(user);
        }
        [HttpPost("AssignRole")]
        [AdminLevel]
        public async Task<ActionResult> AssignRole(RoleAssignment roleAssignment)
        {

            var result = await _repo.AssignRole(roleAssignment);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { result.Message});
        }
        [HttpDelete]
        [AdminLevel]
        public async Task<ActionResult> Delete(int id)
        {
            var repsonse = await _repo.RemoveAsync(id);
            return Ok(repsonse.Data);
        }
    }
}
