using System;
using Ardalis.Specification;

namespace Orion.Manager.Core.Students.Read.GetStudentById
{
    public sealed class GetStudentByIdSpecification : Specification<Student>
    {
        public GetStudentByIdSpecification(Guid id)
        {
            Query
                .Include(e => e.PersonalInfo)
                .Where(e => e.Id == id);
        }
    }
}