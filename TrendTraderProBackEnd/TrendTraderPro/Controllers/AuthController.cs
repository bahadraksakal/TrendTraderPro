using Entities.Users;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models.AuthModels;
using Models.SwaggerExampleModels;
using Repositorys.UserRepositorys;
using Services.AuthServices;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace TrendTraderPro.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IValidator<UserDTO> _userDTOValidator;

        public AuthController(
            IUserRepository userRepository,
            IAuthService authService,
            IValidator<UserDTO> userDTOValidator)
        {
            _userRepository = userRepository;
            _authService = authService;
            _userDTOValidator = userDTOValidator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Register User", Description = "The user can register with this method. Name, Password, Email and Tel is required.")]
        [SwaggerRequestExample(typeof(UserDTO), typeof(RegisterExampleModel))]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            try
            {
                var validationResult = _userDTOValidator.Validate(user, options => options.IncludeRuleSets("Register"));
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                UserDTO newUser = await _userRepository.AddUserAsync(user.Name, user.Password, user.Email, user.Tel, "User");
                return StatusCode(StatusCodes.Status201Created, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest("AuthController-Register Hata:" + ex.InnerException?.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Login User", Description = "The user can login with this method. Name and Password is required.")]
        [SwaggerRequestExample(typeof(UserDTO), typeof(LoginExampleModel))]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            try
            {
                var validationResult = _userDTOValidator.Validate(user , options => options.IncludeRuleSets("Login"));
                if(!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                
                LoginResponseModel loginResponse = await _authService.CheckCredentialsAsync(user.Name ?? "", user.Password ?? "");
                if (loginResponse.userDto != null)
                {
                    return Ok(loginResponse);
                }
                return BadRequest("Kullanıcı Adı Veya Şifre Hatalı");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "AuthController : Login metodunda hata!" + ex.Message);
            }

        }
    }
}
