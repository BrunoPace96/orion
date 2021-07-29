using System;
using Orion.Core.Domain;
using Orion.Core.Domain.Contracts;
using Orion.Manager.Core.Common.ValueObjects;

namespace Orion.Manager.Core.Students
{
    public class Student: EntityBase, IAggregateRoot
    {
        public Guid PersonalInfoId { get; set; }
        public PersonalInfo PersonalInfo { get; }
        public RegistrationNumber RegistrationNumber { get; }

        private Student() {}

        private Student(PersonalInfo personalInfo)
        {
            PersonalInfo = personalInfo;
            RegistrationNumber = new RegistrationNumber();
        }

        public static Student Create(PersonalInfo personalInfo) => 
            new(personalInfo);
    }
}