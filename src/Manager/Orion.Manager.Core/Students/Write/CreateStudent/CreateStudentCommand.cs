using MediatR;

namespace Orion.Manager.Core.Students.Write.CreateStudent
{
    public record CreateStudentCommand: IRequest<CreateStudentResult>
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}