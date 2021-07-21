using System.Collections.Generic;
using System.Linq;
using Orion.Core.Domain;
using Orion.OperationResult.Implementations;

namespace Orion.Manager.Core.Common.ValueObjects
{
    public class Cpf: ValueObject
    {
        public string Value { get; }
        
        private Cpf(string value)
        {
            Value = value;
        }

        public static ValueObjectResult<Cpf> Create(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return new List<string> { "Cannot be null or empty!" };
            
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            var fails = new List<string>();
            
            if (cpf.Length != 11)
                fails.Add("Must contains 11 digits!");
            
            if (cpf.Any(e => !char.IsDigit(e)))
                fails.Add("Must contain only digits!");

            if (fails.Count > 0)
                return fails;
            
            if(!IsValid(cpf))
                return new List<string> { "Invalid!" }; 
            
            return new Cpf(cpf);
        }

        private static bool IsValid(string cpf)
        {
            var position = 0;
            var totalD1 = 0;
            var totalD2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            var identicalDigits = true;
            var lastDigit = -1;

            foreach (var digit in from c in cpf where char.IsDigit(c) select c - '0')
            {
                if (position != 0 && lastDigit != digit)
                    identicalDigits = false;

                lastDigit = digit;
                switch (position)
                {
                    case < 9:
                        totalD1 += digit * (10 - position);
                        totalD2 += digit * (11 - position);
                        break;
                    case 9:
                        dv1 = digit;
                        break;
                    case 10:
                        dv2 = digit;
                        break;
                }

                position++;
            }

            if (position > 11)
                return false;

            if (identicalDigits)
                return false;
                
            var d1 = totalD1 % 11;
            d1 = d1 < 2  ? 0 : 11 - d1;

            if (dv1 != d1)
                return false;

            totalD2 += d1 * 2;
            var d2 = totalD2 % 11;
            d2 = d2 < 2 ? 0 : 11 - d2;

            return dv2 == d2;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator Cpf(string value) => 
            Create(value).Value;
        
        public static implicit operator string(Cpf cpf) => 
            cpf.Value;

        public override string ToString() => Value;
    }
}