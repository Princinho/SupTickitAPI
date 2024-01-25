using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Suptickit.Application;
using SupTickit.API.DTOs;
using SupTickit.Domain;

namespace SupTickit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;
        public AuthenticationController(IAuthRepository authRepository, IMapper mapper )
        {
            _authRepo = authRepository;
            _mapper = mapper;
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
            
            var token = await _authRepo.Login(userDto.Username, userDto.Password);
            if (token == null) { return BadRequest("Invalid credentials"); }
            return Ok(token);
        }
    }
}
