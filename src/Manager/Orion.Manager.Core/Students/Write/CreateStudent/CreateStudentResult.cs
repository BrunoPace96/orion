using System;

namespace Orion.Manager.Core.Students.Write.CreateStudent
{
    public record CreateStudentResult
    {
        public Guid Id { get; init; }
        public string RegistrationNumber { get; init; }
    }
}