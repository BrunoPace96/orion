using Orion.Core.Domain;
using Orion.Core.Domain.Contracts;
using Orion.Manager.Core.Common.ValueObjects;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Students
{
    public class Student: EntityBase, IAggregateRoot
    {
        public Name Name { get; }
        public Cpf Cpf { get; }
        public Email Email { get; }
        public Phone Phone { get; }

        private Student() {}

        private Student(Name name, Cpf cpf, Email email, Phone phone)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Phone = phone;
        }

        public static EntityResult<Student> Create(
            Name name,
            Cpf cpf,
            Email email,
            Phone phone
        ) => new Student(name, cpf, email, phone);
    }
}