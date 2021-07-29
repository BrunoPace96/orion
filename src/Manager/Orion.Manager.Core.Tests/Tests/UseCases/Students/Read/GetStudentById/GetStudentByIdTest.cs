using System;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Orion.DataContracts.Queries;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Students;
using Orion.Manager.Core.Students.Read.GetStudentById;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Students.Read.GetStudentById
{
    public class GetStudentByIdTest: BaseTest<Student>
    {
        public GetStudentByIdTest(ApplicationFixture fixture): base(fixture) {}
        
        [Fact]
        public async Task Not_Founded()
        {
            var query = new ByIdQuery<GetStudentByIdResult>(Guid.NewGuid());
            var result = await Mediator.Send(query);
            Assert.Null(result);
            Assert.True(Validator.HasNotFoundErrors());
        }

        [Fact]
        public async Task Ok()
        {
            var faker = new Faker(LocaleConstants.Locale);
            
            var name = Name.Create(faker.Name.FullName());
            var cpf = Cpf.Create(faker.Person.Cpf(false));
            var email = Email.Create(faker.Person.Email);
            var phone = Phone.Create(faker.Phone.PhoneNumber("419########"));

            var entity = Student.Create(name, cpf, email, phone);

            var entityId = await GenerateAndAsync(entity);
            var query = new ByIdQuery<GetStudentByIdResult>(entityId);
            var result = await Mediator.Send(query);

            Assert.NotNull(result);
            Assert.False(Validator.HasErrors());
        }
    }
}