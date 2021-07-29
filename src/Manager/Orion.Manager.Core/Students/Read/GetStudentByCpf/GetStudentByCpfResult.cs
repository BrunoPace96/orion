using System;

namespace Orion.Manager.Core.Students.Read.GetStudentByCpf
{
    public record GetStudentByCpfResult
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string RegistrationNumber { get; init; }
    }
}