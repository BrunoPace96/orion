using System;
using System.Threading.Tasks;
using Orion.DataContracts.Queries;
using Orion.Manager.Core.Students;
using Orion.Manager.Core.Students.Read.GetStudentById;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Xunit;
using Xunit.Abstractions;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Students.Read.GetStudentById
{
    public class GetStudentByIdTest: BaseTest<Student>
    {
        private readonly ITestOutputHelper _output;
        public GetStudentByIdTest(ApplicationFixture fixture, ITestOutputHelper output): 
            base(fixture)
        {
            _output = output;
        }
        
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
            var student = StudentGenerator.Generate();

            var entityId = await SaveEntityAsync(student);
            var query = new ByIdQuery<GetStudentByIdResult>(entityId);
            var result = await Mediator.Send(query);

            _output.PrintResult(result);
            Assert.NotNull(result);
            Assert.False(Validator.HasErrors());
        }
    }
}