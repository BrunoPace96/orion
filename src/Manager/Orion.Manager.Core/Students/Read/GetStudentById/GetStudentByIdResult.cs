using System;

namespace Orion.Manager.Core.Students.Read.GetStudentById
{
    public record GetStudentByIdResult
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Cpf { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
    }
}