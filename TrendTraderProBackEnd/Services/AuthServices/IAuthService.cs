using Entities.Users;
using Models.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.AuthServices
{
    public interface IAuthService
    {
        Task<LoginResponseModel> CheckCredentialsAsync(string userName, string userPassword);
        string CreateToken(UserDTO user);
    }
}
