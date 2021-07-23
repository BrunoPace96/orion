using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Extensions;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Orion.Manager.Core.Users;
using Orion.Manager.Core.Users.Read.GetUserByCpf;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Users.Read.GetUserByCpf
{
    public class GetUserByCpfTest: BaseTest<User>
    {
        private readonly Faker _faker;

        public GetUserByCpfTest(ApplicationFixture fixture) : 
            base(fixture)
        {
            _faker = new Faker(LocaleConstants.Locale);
        }
        
        [Fact]
        public async Task Not_Founded()
        {
            var query = new GetUserByCpfQuery(_faker.Person.Cpf(false));
            var result = await Mediator.Send(query);
            Assert.Null(result);
            Assert.True(Validator.HasNotFoundErrors());
        }

        [Fact]
        public async Task Ok()
        {
            var entity = User.Create(
                _faker.Name.FullName(),
                _faker.Person.Cpf(false),
                _faker.Person.Email,
                _faker.Phone.PhoneNumber("419########")
            );

            await GenerateAndAsync(entity);
            var query = new GetUserByCpfQuery(entity.Value.Cpf);
            var result = await Mediator.Send(query);

            Assert.NotNull(result);
            Assert.False(Validator.HasErrors());
        }
    }
}