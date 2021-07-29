using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orion.DataContracts.Queries;
using Orion.DataContracts.Results;
using Orion.Manager.API.Common.Endpoints;
using Orion.Manager.Core.Students.Read.GetStudentById;
using Swashbuckle.AspNetCore.Annotations;

namespace Orion.Manager.API.Students.Read
{
    [Route("student")]
    public class GetStudentByIdEndpoint: BaseEndpointAsync
        .WithRequest<Guid>
        .WithResponse<GetStudentByIdResult>
    {
        private readonly IMediator _mediator;

        public GetStudentByIdEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("/{id:guid}")]
        [SwaggerOperation(
            Summary = "Get Student By Id",
            OperationId = "Student.GetById",
            Tags = new[] { "Student" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status404NotFound)]
        public override async Task<ActionResult<GetStudentByIdResult>> HandleAsync(
            [FromRoute] Guid id, 
            CancellationToken cancellationToken = new()
        )
        {
            var query = new ByIdQuery<GetStudentByIdResult>(id);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
    }
}