using Bogus;
using Bogus.Extensions.Brazil;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Students;
using Orion.Manager.Core.Tests.Common.Constants;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Students
{
    public class StudentGenerator
    {
        public static Student Generate()
        {
            var faker = new Faker(LocaleConstants.Locale);
            
            var name = Name.Create(faker.Name.FullName());
            var cpf = Cpf.Create(faker.Person.Cpf(false));
            var email = Email.Create(faker.Person.Email);
            var phone = Phone.Create(faker.Phone.PhoneNumber("419########"));

            var personalInfo = PersonalInfo.Create(name, cpf, email, phone);
            
            return Student.Create(personalInfo);
        }
    }
}