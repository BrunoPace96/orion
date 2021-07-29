using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Students;
using Orion.Manager.Core.Students.Read.GetStudentByCpf;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Students.Read.GetStudentByCpf
{
    public class GetStudentByCpfTest: BaseTest<Student>
    {
        private readonly Faker _faker;

        public GetStudentByCpfTest(ApplicationFixture fixture) : 
            base(fixture)
        {
            _faker = new Faker(LocaleConstants.Locale);
        }
        
        [Fact]
        public async Task Not_Founded()
        {
            var query = new GetStudentByCpfQuery(_faker.Person.Cpf(false));
            var result = await Mediator.Send(query);
            Assert.Null(result);
            Assert.True(Validator.HasNotFoundErrors());
        }

        [Fact]
        public async Task Ok()
        {
            var name = Name.Create(_faker.Name.FullName());
            var cpf = Cpf.Create(_faker.Person.Cpf(false));
            var email = Email.Create(_faker.Person.Email);
            var phone = Phone.Create(_faker.Phone.PhoneNumber("419########"));
            
            var entity = Student.Create(name, cpf, email, phone);

            await GenerateAndAsync(entity);
            var query = new GetStudentByCpfQuery(entity.Value.Cpf);
            var result = await Mediator.Send(query);

            Assert.NotNull(result);
            Assert.False(Validator.HasErrors());
        }
    }
}