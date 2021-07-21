using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orion.DataContracts.Results;
using Orion.Manager.API.Common.Endpoints;
using Orion.Manager.Core.Users.Write.CreateUser;
using Swashbuckle.AspNetCore.Annotations;

namespace Orion.Manager.API.Users.Write
{
    [Route("user")]
    public class CreateUserEndpoint: BaseEndpointAsync
        .WithRequest<CreateUserCommand>
        .WithResponse<CreateUserResult>
    {
        private readonly IMediator _mediator;

        public CreateUserEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create User",
            OperationId = "User.Create",
            Tags = new[] { "User" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status404NotFound)]
        public override async Task<ActionResult<CreateUserResult>> HandleAsync(
            [FromBody] CreateUserCommand command, 
            CancellationToken cancellationToken = new()
        )
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}