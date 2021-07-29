using Ardalis.Specification;

namespace Orion.Manager.Core.Students.Read.GetStudentByCpf
{
    public sealed class GetStudentByCpfSpecification : Specification<Student>
    {
        public GetStudentByCpfSpecification(string cpf)
        {
            Query.Where(e => e.Cpf.Value == cpf);
        }
    }
}