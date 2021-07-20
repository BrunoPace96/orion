using System.Linq;
using System.Net;
using Orion.DomainValidation.Domain;

namespace Orion.Manager.Core.Tests.Common.Extensions
{
    public static class DomainValidatorExtensions
    {
        public static bool HasNotFoundErrors(this IDomainValidationProvider validator) =>
            validator.GetErrors().Any(e => e.StatusCode == HttpStatusCode.NotFound);
    }
}