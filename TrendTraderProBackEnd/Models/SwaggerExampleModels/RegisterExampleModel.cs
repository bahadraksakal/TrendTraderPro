using Entities.Users;
using Swashbuckle.AspNetCore.Filters;

namespace Models.SwaggerExampleModels
{
    public class RegisterExampleModel : IExamplesProvider<UserDTO>
    {
        public UserDTO GetExamples()
        {
            return new UserDTO
            {
                Name = "Deneme1",
                Password = "Deneme_1",
                Email = "deneme1@gmail.com",
                Tel = "05458876655"
            };
        }
    }
}
