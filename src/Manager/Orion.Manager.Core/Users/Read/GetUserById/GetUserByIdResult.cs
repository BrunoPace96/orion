using System;

namespace Orion.Manager.Core.Users.Read.GetUserById
{
    public record GetUserByIdResult
    (
        Guid Id,
        string Name,
        string Cpf,
        string Email,
        string Phone
    );
}