using System;

namespace Orion.Manager.Core.Students.Read.GetStudentById
{
    public record GetStudentByIdResult
    (
        Guid Id,
        string Name,
        string Cpf,
        string Email,
        string Phone
    );
}