using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Todo_List_API.DTOs;
using Todo_List_API.Services.Interfaces;

namespace Todo_List_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly static string nameAssembly = Assembly.GetExecutingAssembly().GetName().Name!;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(registerUserDTO);
                return Ok(new { Token = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameAssembly} - {MethodBase.GetCurrentMethod()} - {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                var result = await _userService.LoginUserAsync(loginUserDTO);
                return Ok(new {Token = result});
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameAssembly} - {MethodBase.GetCurrentMethod()} - {ex.Message}");
                return Unauthorized(new {message = ex.Message});
            }
        }


    }
}
