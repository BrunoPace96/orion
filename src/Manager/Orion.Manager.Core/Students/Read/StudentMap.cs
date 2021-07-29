using AutoMapper;
using Orion.Manager.Core.Students.Read.GetStudentByCpf;
using Orion.Manager.Core.Students.Read.GetStudentById;
using Orion.Manager.Core.Students.Write.CreateStudent;

namespace Orion.Manager.Core.Students.Read
{
    public class StudentMap: Profile
    {
        public StudentMap()
        {
            CreateMap<Student, CreateStudentResult>()
                .ForMember(e => e.RegistrationNumber, i => i.MapFrom(e => e.RegistrationNumber.Value));

            CreateMap<Student, GetStudentByIdResult>()
                .ForMember(e => e.Name, i => i.MapFrom(e => e.PersonalInfo.Name))
                .ForMember(e => e.Cpf, i => i.MapFrom(e => e.PersonalInfo.Cpf))
                .ForMember(e => e.Email, i => i.MapFrom(e => e.PersonalInfo.Email))
                .ForMember(e => e.Phone, i => i.MapFrom(e => e.PersonalInfo.Phone));
            
            CreateMap<Student, GetStudentByCpfResult>()
                .ForMember(e => e.Name, i => i.MapFrom(e => e.PersonalInfo.Name))
                .ForMember(e => e.RegistrationNumber, i => i.MapFrom(e => e.RegistrationNumber.Value));
        }
    }
}