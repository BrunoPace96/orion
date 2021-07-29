using MediatR;

namespace Orion.Manager.Core.Students.Read.GetStudentByCpf
{
    public record GetStudentByCpfQuery(string Cpf): IRequest<GetStudentByCpfResult>;
}