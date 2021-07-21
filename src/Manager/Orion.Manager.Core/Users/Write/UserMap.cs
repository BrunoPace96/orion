using AutoMapper;
using Orion.Manager.Core.Users.Write.CreateUser;

namespace Orion.Manager.Core.Users.Write
{
    public class UserMap: Profile
    {
        public UserMap()
        {
            CreateMap<User, CreateUserResult>();
        }
    }
}