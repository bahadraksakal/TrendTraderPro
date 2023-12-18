using Entities.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Models.SwaggerExampleModels
{
    public class LoginExampleModel: IExamplesProvider<UserDTO>
    {
        public UserDTO GetExamples()
        {
            return new UserDTO
            {
                Name = "admin",
                Password = "admin"
            };
        }
    }
}
