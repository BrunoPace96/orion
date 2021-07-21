using FluentValidation;
using Orion.DomainValidation.Extensions;
using Orion.Manager.Core.Common.ValueObjects;

namespace Orion.Manager.Core.Users.Write.CreateUser
{
    public class CreateUserValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(e => e.Name).MustBeValueObject(Name.Create);
            RuleFor(e => e.Cpf).MustBeValueObject(Cpf.Create);
            RuleFor(e => e.Email).MustBeValueObject(Email.Create);
            RuleFor(e => e.Phone).MustBeValueObject(Phone.Create);
        }
    }
}