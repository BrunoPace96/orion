using System.Collections.Generic;

namespace Orion.DataContracts.Results
{
    public record ValidationFailedResult(
        string Message, 
        IList<ValidationErrorResult> Errors
    );
}