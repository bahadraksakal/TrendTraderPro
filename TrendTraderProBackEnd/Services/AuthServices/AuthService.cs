using AutoMapper;
using Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.AuthModels;
using Repositorys.UserRepositorys;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<IAuthService> _logger;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper, ILogger<IAuthService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LoginResponseModel> CheckCredentialsAsync(string userName, string userPassword)
        {
            UserDTO? userDTO = await _userRepository.GetUserByNameAndPass(userName,userPassword);
            LoginResponseModel loginResponseModel = new();
            if (userDTO != null)
            {
                string token=CreateToken(userDTO);    
                loginResponseModel.userDto = userDTO;
                loginResponseModel.token = token;
                _logger.LogWarning($"AuthService-CheckCredentialsAsync: {userDTO.Name} giriş yaptı.");
            }
            else
            {
                _logger.LogWarning($"AuthService-CheckCredentialsAsync: {userName} hatalı giriş denemesi yaptı.");
            }            
            return loginResponseModel;
        }

        public string CreateToken(UserDTO user)
        {

            Claim[] Claims = new[]
                    {
                        new Claim("userName", user.Name ?? ""),
                        new Claim("userId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role ?? ""),
                    };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SigningKey"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configuration["JwtConfig:Issuer"],
                    audience: _configuration["JwtConfig:Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddHours(1),
                    notBefore: DateTime.Now, 
                    signingCredentials: credentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            _logger.LogWarning($"AuthService-CreateToken: Token oluşturuldu: {token}");
            return token;
        }
    }
}
