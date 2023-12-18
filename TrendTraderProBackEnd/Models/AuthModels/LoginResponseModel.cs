using Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AuthModels
{
    public class LoginResponseModel
    {
        public UserDTO? userDto { get; set; }
        public string? token { get; set; } 
    }
}
