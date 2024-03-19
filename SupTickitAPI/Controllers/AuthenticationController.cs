using AutoMapper;
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
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IRoleAssignmentRepository _rolesRepo;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepo;
        public AuthenticationController(IAuthRepository authRepository,IUsersRepository usersRepo, IMapper mapper, IRoleAssignmentRepository rolesRepo )
        {
            _authRepo = authRepository;
            _mapper = mapper;
            _usersRepo=usersRepo;
            _rolesRepo=rolesRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<int>>Register(UserRegisterDto userDto)
        {
            if(userDto.Password!=userDto.PasswordConfirmation)
            {
                return BadRequest("Passwords do not match");
            }
            var user = _mapper.Map<User>(userDto);
            var userId=await _authRepo.Register(user,userDto.Password);
            return Ok(userId);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginOutDTO>> Login(UserLoginDto userDto)
        {
            
            var users = await _usersRepo.GetAllAsync();
            if (users.Count()== 0)
            {
                SeedAdminUser();
            }
            var token = await _authRepo.Login(userDto.Username, userDto.Password);
            if (token == null) { return BadRequest("Invalid credentials"); }
            return Ok(token);
        }
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<UserLoginOutDTO>> ChangePassword(ChangePasswordDTO userDto)
        {
            var users = await _usersRepo.GetAllAsync();
            if (users.Count() == 0)
            {
                SeedAdminUser();
            }
            await _authRepo.ChangePassword(userDto.Username, userDto.OldPassword,userDto.Password);
            
            return Ok();
        }
        [HttpPost("ResetUserPassword/{username}")]
        [AdminLevel]
        public async Task<ActionResult<UserLoginOutDTO>> ResetUserPassword(string username)
        {
            try
            {
            await _authRepo.ResetPassword(username);
            
            return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        public async void SeedAdminUser()
        {
            var adminUser = new User { Firstname = "Admin", Lastname = "User", Username = "SysAdmin", RoleAssignments = new List<RoleAssignment>() };
            var adminUserId=await _authRepo.Register(adminUser, "Admin@2023");
            await _rolesRepo.AssignRole(new RoleAssignment { RoleId = RoleEnum.Administrator,UserId= adminUserId, StartDate=DateTime.UtcNow });
        }
        [HttpGet("roles")]
        public async Task<ActionResult<IEnumerable< RoleAssignment>>> GetAllRoles()
        {
            return Ok(await _rolesRepo.GetAllAsync());
        }
        [HttpGet("activeRoles")]
        public async Task<ActionResult<IEnumerable< RoleAssignment>>> GetActiveRoles()
        {
            return Ok(await _rolesRepo.GetAllActiveAsync());
        }
    }
}
