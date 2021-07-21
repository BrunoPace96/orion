using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using Orion.Manager.Core.Tests.Common.Constants;
using Orion.Manager.Core.Tests.Common.Fixtures;
using Orion.Manager.Core.Tests.Common.Tests;
using Orion.Manager.Core.Users;
using Orion.Manager.Core.Users.Write.CreateUser;
using Xunit;

namespace Orion.Manager.Core.Tests.Tests.UseCases.Users.Write.CreateUser
{
    public class CreateUserTest: BaseTest<User>
    {
        private readonly CreateUserCommand _command;
        public CreateUserTest(ApplicationFixture fixture): base(fixture)
        {
            _command = new Faker<CreateUserCommand>(LocaleConstants.Locale).Rules((f, o) =>
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
            var exists = await Repository.CheckIfExistsAsync(result.Id);
            Assert.NotNull(result);
            Assert.True(exists);
        }
    }
}