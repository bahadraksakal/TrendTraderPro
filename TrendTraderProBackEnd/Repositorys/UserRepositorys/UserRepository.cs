using AutoMapper;
using DbContexts.DbContextTrendTraderPro;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repositorys.UserRepositorys
{
    public class UserRepository : IUserRepository
    {
        private readonly TrendTraderProDbContext _trendTraderProDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<IUserRepository> _logger;

        public UserRepository(TrendTraderProDbContext trendTraderProDbContext, IMapper mapper, ILogger<IUserRepository> logger)
        {
            _trendTraderProDbContext = trendTraderProDbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDTO> AddUserAsync(string? userName, string? userPassword, string? userEmail, string? userTel, string? userRole)
        {
            User newUser = new User() { Name = userName, Password = userPassword, Email = userEmail, Tel = userTel, Role = userRole };
            await _trendTraderProDbContext.Users.AddAsync(newUser);
            await _trendTraderProDbContext.SaveChangesAsync();
            _logger.LogWarning($"Kullanıcı Başarıyla Eklendi: [Name:{newUser.Name} - Role:{newUser.Role}]");
            return _mapper.Map<UserDTO>(newUser); 
        }
        public async Task<UserDTO> GetUserByNameAndPass(string userName,string userPassword)
        {
            User? user = await _trendTraderProDbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Name == userName && user.Password == userPassword);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
