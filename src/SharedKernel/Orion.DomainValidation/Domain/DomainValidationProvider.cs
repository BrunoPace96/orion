using System.Collections.Generic;
using System.Linq;
using System.Net;
using Orion.DomainValidation.DataContracts;

namespace Orion.DomainValidation.Domain
{
    public class DomainValidationProvider : IDomainValidationProvider
    {
        private readonly List<DomainValidationNotification> _validations = new();

        public bool HasErrors() => 
            _validations.Any();

        public bool HasNoErrors() => 
            !HasErrors();

        public List<DomainValidationNotification> GetErrors() => 
            _validations;

        public void AddValidationError(string message, string field = "default") =>
            AddValidationError(new DomainValidationNotification(message, field));
        public void AddValidationError(DomainValidationNotification validationNotification) => 
            _validations.Add(validationNotification);

        public void AddValidationErrors(IEnumerable<DomainValidationNotification> validations) =>
            _validations.AddRange(validations);
        
        public void AddUnauthorizedError(string message = "Unauthorized!") => 
            AddValidationError(new DomainValidationNotification(HttpStatusCode.Unauthorized, message));
        
        public void AddForbiddenError(string message = "Forbidden!") => 
            AddValidationError(new DomainValidationNotification(HttpStatusCode.Forbidden, message));
        
        public void AddNotFoundError(string message = "Not Found!") => 
            AddValidationError(new DomainValidationNotification(HttpStatusCode.NotFound, message));
    }
}