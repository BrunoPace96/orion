using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Newtonsoft.Json;
using Orion.Manager.Core.Students;
using Orion.Manager.Core.Students.Write.CreateStudent;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Xunit;
using Xunit.Abstractions;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Students.Write.CreateUser
{
    public class CreateStudentTest: BaseTest<Student>
    {
        private readonly ITestOutputHelper _output;
        private readonly CreateStudentCommand _command;
        public CreateStudentTest(ApplicationFixture fixture, ITestOutputHelper output): base(fixture)
        {
            _output = output;
            _command = new Faker<CreateStudentCommand>(LocaleConstants.Locale).Rules((f, o) =>
            {
                o.Cpf = f.Person.Cpf(false);
                o.Email = f.Person.Email;
                o.Phone = f.Phone.PhoneNumber("419########");
                o.Name = f.Name.FullName();
            });
        }
        
        [Fact]
        public async Task Ok()
        {
            var result = await Mediator.Send(_command);
            var exists = await ReadOnlyRepository.CheckIfExistsAsync(result.Id);
            _output.PrintResult(result);
            Assert.NotNull(result);
            Assert.True(exists);
        }
    }
}