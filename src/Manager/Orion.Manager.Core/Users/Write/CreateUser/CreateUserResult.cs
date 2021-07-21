using System;

namespace Orion.Manager.Core.Users.Write.CreateUser
{
    public record CreateUserResult(
        Guid Id,
        string Name,
        string Cpf,
        string Email,
        string Phone
    );
}