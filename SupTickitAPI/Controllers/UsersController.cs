using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using Suptickit.Domain.Enums;
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
        private readonly IRoleAssignmentRepository _rolesRepo;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepository repo, IMapper mapper, IAuthRepository authRepo,IRoleAssignmentRepository rolesRepo)
        {
            _repo = repo;
            _rolesRepo = rolesRepo;
            _authRepo = authRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsersGetAllDTO>>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            var usersDTOs = _mapper.Map<IEnumerable<UsersGetAllDTO>>(users);
            foreach (var userDTO in usersDTOs)
            {
                if (userDTO.RoleAssignments != null)
                {
                    userDTO.Roles = new HashSet<RoleEnum>( userDTO.RoleAssignments.Where(r => r.StartDate < DateTime.UtcNow && r.ExpiryDate > DateTime.UtcNow).Select(r => r.RoleId).ToList()).ToList();
                }
            }
                return Ok(usersDTOs);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsersGetAllDTO>> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            var usersDTO = _mapper.Map<UsersGetAllDTO>(user);
            return Ok(usersDTO);
        }
        [HttpGet("byUsername/{username}")]
        [Authorize]
        public async Task<ActionResult<UsersGetAllDTO>> GetByIdUsernameAsync(string username)
        {
            var user = await _repo.GetByUserNameAsync(username);
            var usersDTO = _mapper.Map<UsersGetAllDTO>(user);
            return Ok(usersDTO);
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

            var result = await _rolesRepo.AssignRole(roleAssignment);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { result.Message });
        }
        [HttpDelete("UnAssignRole/{id}")]
        [AdminLevel]
        public async Task<ActionResult> UnAssignRole(int id)
        {

            var result = await _rolesRepo.UnAssignRole(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(new { result.Message });
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
