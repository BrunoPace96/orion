using System;

namespace Orion.Manager.Core.Students.Write.CreateStudent
{
    public record CreateStudentResult(
        Guid Id,
        string Name,
        string Cpf,
        string Email,
        string Phone
    );
}