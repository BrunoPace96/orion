using Orion.Core.Domain;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.Manager.Core.Users.Write.SendConfirmationCode;
using Orion.OperationResult.Helpers;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Users
{
    public class User: EntityBase
    {
        public Name Name { get; }
        public Cpf Cpf { get; }
        public Email Email { get; }
        public Phone Phone { get; }

        private User() {}

        private User(Name name, Cpf cpf, Email email, Phone phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
        }
        
        public override void Created()
        {
            AddDomainEvent(new SendConfirmationCodeNotification(Phone));
            base.Created();
        }
        
        public static EntityResult<User> Create(Name name, Cpf cpf, Email email, Phone phone)
        {
            var result = ValidationsHelper.GetFailValidations(name, cpf, email, phone);
            if (result.Count > 0)
                return result;

            return new User(name, cpf, email, phone);
        }
    }
}