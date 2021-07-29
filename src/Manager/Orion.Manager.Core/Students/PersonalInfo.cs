using Orion.Core.Domain;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Students
{
    public class PersonalInfo: EntityBase
    {
        public Name Name { get; }
        public Cpf Cpf { get; }
        public Email Email { get; }
        public Phone Phone { get; }

        private PersonalInfo() {}

        private PersonalInfo(Name name, Cpf cpf, Email email, Phone phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
        }

        public static EntityResult<PersonalInfo> Create(
            Name name,
            Cpf cpf,
            Email email,
            Phone phone
        ) => new PersonalInfo(name, cpf, email, phone);
    }
}