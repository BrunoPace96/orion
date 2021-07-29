using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orion.DataContracts.Results;
using Orion.Manager.API.Common.Endpoints;
using Orion.Manager.Core.Students.Write.CreateStudent;
using Swashbuckle.AspNetCore.Annotations;

namespace Orion.Manager.API.Students.Write
{
    [Route("student")]
    public class CreateStudentEndpoint: BaseEndpointAsync
        .WithRequest<CreateStudentCommand>
        .WithResponse<CreateStudentResult>
    {
        private readonly IMediator _mediator;

        public CreateStudentEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create Student",
            OperationId = "Student.Create",
            Tags = new[] { "Student" }
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailedResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(MessageResult), StatusCodes.Status404NotFound)]
        public override async Task<ActionResult<CreateStudentResult>> HandleAsync(
            [FromBody] CreateStudentCommand command, 
            CancellationToken cancellationToken = new()
        )
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}