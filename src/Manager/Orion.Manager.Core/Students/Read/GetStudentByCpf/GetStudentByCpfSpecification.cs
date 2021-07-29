using Ardalis.Specification;

namespace Orion.Manager.Core.Students.Read.GetStudentByCpf
{
    public sealed class GetStudentByCpfSpecification : Specification<Student>
    {
        public GetStudentByCpfSpecification(string cpf)
        {
            Query
                .Include(e => e.PersonalInfo)
                .Where(e => e.PersonalInfo.Cpf.Value == cpf);
        }
    }
}