using System;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Orion.DataContracts.Queries;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Orion.Manager.Core.Users;
using Orion.Manager.Core.Users.Read.GetUserById;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Users.Read.GetUserById
{
    public class GetUserByIdTest: BaseTest<User>
    {
        public GetUserByIdTest(ApplicationFixture fixture): base(fixture) {}
        
        [Fact]
        public async Task Not_Founded()
        {
            var query = new ByIdQuery<GetUserByIdResult>(Guid.NewGuid());
            var result = await Mediator.Send(query);
            Assert.Null(result);
            Assert.True(Validator.HasNotFoundErrors());
        }

        [Fact]
        public async Task Ok()
        {
            var faker = new Faker(LocaleConstants.Locale);

            var entity = User.Create(
                faker.Name.FullName(),
                faker.Person.Cpf(false),
                faker.Person.Email,
                faker.Phone.PhoneNumber("419########")
            );

            var entityId = await GenerateAndAsync(entity);
            var query = new ByIdQuery<GetUserByIdResult>(entityId);
            var result = await Mediator.Send(query);

            Assert.NotNull(result);
            Assert.False(Validator.HasErrors());
        }
    }
}