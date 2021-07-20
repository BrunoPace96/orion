using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Orion.DataContracts.Results;
using Orion.DomainValidation.Domain;

namespace Orion.Manager.API.Common.Filters
{
    public class DomainValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) {}

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var validator = context.HttpContext.RequestServices
                .GetRequiredService<IDomainValidationProvider>();

            if (validator.HasNoErrors())
                return;

            bool CheckForPrioritizedFails()
            {
                var statusCodes = new List<HttpStatusCode>
                {
                    HttpStatusCode.Unauthorized,
                    HttpStatusCode.Forbidden,
                    HttpStatusCode.NotFound
                };

                var domainValidation = statusCodes
                    .Select(e => validator.GetErrors().FirstOrDefault(x => x.StatusCode == e))
                    .FirstOrDefault(e => e != null);

                if (domainValidation != null)
                {
                    context.Result = new ObjectResult(new MessageResult(domainValidation.Message))
                    {
                        StatusCode = (int) domainValidation.StatusCode
                    };

                    return true;
                }

                return false;
            }

            if (CheckForPrioritizedFails()) return;

            void CheckForBadRequestFails()
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count != 0)
                    .Select(e => new ValidationErrorResult
                    {
                        Field = e.Key,
                        Messages = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    })
                    .ToList();

                if (errors.Count != 0)
                {
                    var validationFailedResult = new ValidationFailedResult("Fail to save!", errors);
                    context.Result = new BadRequestObjectResult(validationFailedResult);
                }
            }

            CheckForBadRequestFails();
        }
    }
}