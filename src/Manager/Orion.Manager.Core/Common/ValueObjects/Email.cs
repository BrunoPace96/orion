using System.Collections.Generic;
using System.Text.RegularExpressions;
using Orion.Core.Domain;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Common.ValueObjects
{
    public class Email: ValueObject
    {
        public string Value { get; }
        
        private Email(string value)
        {
            Value = value;
        }

        public static ValueObjectResult<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new List<string> { "Cannot be null or empty!" };

            var fails = new List<string>();
            
            if (email.Length is < 10 or > 150)
                fails.Add("Must have between 10 and 150 characters!");
            
            if (Regex.IsMatch(email, @"^(.+)@(.+)$") == false)
                fails.Add("Invalid!");
        
            return fails.Count > 0 ? 
                fails: 
                new Email(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static implicit operator Email(string value) => 
            Create(value).Value;
        
        public static implicit operator string(Email email) => 
            email.Value;
        
        public override string ToString() => Value;
    }
}