using System.Collections.Generic;
using System.Linq;
using Orion.Core.Domain;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Common.ValueObjects
{
    public class Phone: ValueObject
    {
        public string Value { get; }
        
        private Phone(string value)
        {
            Value = value;
        }

        public static ValueObjectResult<Phone> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return new List<string> { "Cannot be null or empty!" };

            phone = phone.Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace(" ", "");

            var fails = new List<string>();
            
            if (phone.Length != 11)
                fails.Add("Must contains 11 digits!");
            
            if (phone.Any(e => !char.IsDigit(e)))
                fails.Add("Must contain only digits!");
        
            return fails.Count > 0 ? 
                fails: 
                new Phone(phone);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static implicit operator Phone(string value) => 
            Create(value).Value;
        
        public static implicit operator string(Phone phone) => 
            phone.Value;
        
        public override string ToString() => Value;
    }
}