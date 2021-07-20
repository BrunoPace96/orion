using System.Collections.Generic;
using System.Linq;
using Orion.OperationResult.Implementations;

namespace Orion.OperationResult.Helpers
{
    public static class ValidationsHelper
    {
        public static Dictionary<string, List<string>> GetFailValidations(
            params ValueObjectResult<object>[] items
        )
        {
            var result = new Dictionary<string, List<string>>();
            items.Where(e => e.Failure)
                .ToList()
                .ForEach(e => result.Add(nameof(e), e.Errors));

            return result;
        }
    }
}