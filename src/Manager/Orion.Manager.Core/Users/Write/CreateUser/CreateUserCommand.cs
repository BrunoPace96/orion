using MediatR;

namespace Orion.Manager.Core.Users.Write.CreateUser
{
    public record CreateUserCommand: IRequest<CreateUserResult>
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}