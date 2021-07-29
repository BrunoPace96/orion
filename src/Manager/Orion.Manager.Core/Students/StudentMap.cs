using AutoMapper;
using Orion.Manager.Core.Students.Read.GetStudentByCpf;
using Orion.Manager.Core.Students.Read.GetStudentById;
using Orion.Manager.Core.Students.Write.CreateStudent;

namespace Orion.Manager.Core.Students
{
    public class StudentMap: Profile
    {
        public StudentMap()
        {
            CreateMap<Student, CreateStudentResult>();
            CreateMap<Student, GetStudentByIdResult>();
            CreateMap<Student, GetStudentByCpfResult>();
        }
    }
}