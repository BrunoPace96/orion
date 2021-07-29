using System;
using System.Collections.Generic;
using Orion.Core.Domain;

namespace Orion.Manager.Core.Common.ValueObjects
{
    public class RegistrationNumber: ValueObject
    {
        public string Value { get; }
        
        public RegistrationNumber()
        {
            var date = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            Value = $"RA{date}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}