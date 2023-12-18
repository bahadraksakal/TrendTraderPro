using AutoMapper;
using DbContexts.DbContextTrendTraderPro;
using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorys.UserRepositorys
{
    public interface IUserRepository
    {
        Task<UserDTO> AddUserAsync(string? userName, string? userPassword, string? userEmail, string? userTel, string? userRole);
        Task<UserDTO> GetUserByNameAndPass(string userName, string userPassword);
    }

}
