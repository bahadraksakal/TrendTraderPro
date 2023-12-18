using AutoMapper;

namespace Entities.Users
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<UserDTO, User>();
        }
    }
}
