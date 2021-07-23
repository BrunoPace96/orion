using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orion.DataContracts.Queries;
using Orion.DataContracts.Results;
using Orion.Manager.API.Common.Endpoints;
using Orion.Manager.Core.Users.Read.GetUserById;
using Swashbuckle.AspNetCore.Annotations;

namespace Orion.Manager.API.Users.Read
{
    [Route("user")]
    public class GetUserByIdEndpoint: BaseEndpointAsync
        .WithRequest<Guid>
        .WithResponse<GetUserByIdResult>
    {
        private readonly IMediator _mediator;

        public GetUserByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("/{id:guid}")]
        [SwaggerOperation(
            Summary = "Get User By Id",
            OperationId = "User.GetById",
            Tags = new[] { "User" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status404NotFound)]
        public override async Task<ActionResult<GetUserByIdResult>> HandleAsync(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken = new()
        )
        {
            var query = new ByIdQuery<GetUserByIdResult>(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}