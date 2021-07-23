using AutoMapper;
using Orion.Manager.Core.Users.Read.GetUserById;
using Orion.Manager.Core.Users.Write.CreateUser;

namespace Orion.Manager.Core.Users
{
    public class UserMap: Profile
    {
        public UserMap()
        {
            CreateMap<User, CreateUserResult>();
            CreateMap<User, GetUserByIdResult>();
        }
    }
}