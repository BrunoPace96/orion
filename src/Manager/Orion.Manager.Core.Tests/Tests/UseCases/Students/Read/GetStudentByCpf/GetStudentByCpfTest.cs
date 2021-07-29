using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Newtonsoft.Json;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Students;
using Orion.Manager.Core.Students.Read.GetStudentByCpf;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Xunit;
using Xunit.Abstractions;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Students.Read.GetStudentByCpf
{
    public class GetStudentByCpfTest: BaseTest<Student>
    {
        private readonly ITestOutputHelper _output;
        private readonly Faker _faker;

        public GetStudentByCpfTest(ApplicationFixture fixture, ITestOutputHelper output) : 
            base(fixture)
        {
            _output = output;
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
            var student = StudentGenerator.Generate();

            await SaveEntityAsync(student);
            var query = new GetStudentByCpfQuery(student.PersonalInfo.Cpf);
            var result = await Mediator.Send(query);

            _output.PrintResult(result);
            
            Assert.NotNull(result);
            Assert.False(Validator.HasErrors());
        }
    }
}