using System.Collections.Generic;
using System.Linq;
using Orion.Core.Domain;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Common.ValueObjects
{
    public class Name: ValueObject
    {
        public string Value { get; }
        
        private Name(string value)
        {
            Value = value;
        }

        public static ValueObjectResult<Name> Create(string name)
        {
            name = name?.Trim();
            
            if (string.IsNullOrWhiteSpace(name))
                return new List<string> { "Cannot be null or empty!" };

            var fails = new List<string>();
            
            if (name.Length is < 10 or > 150)
                fails.Add("Must have between 10 and 150 characters!");
            
            if (name.Any(e => !char.IsLetter(e) && !char.IsWhiteSpace(e)))
                fails.Add("Must contain only letters!");
        
            return fails.Count > 0 ? 
                fails: 
                new Name(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static implicit operator Name(string value) => 
            Create(value).Value;
        
        public static implicit operator string(Name name) => 
            name.Value;
        
        public override string ToString() => Value;
    }
}