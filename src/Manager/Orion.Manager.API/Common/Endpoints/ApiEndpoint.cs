using Microsoft.AspNetCore.Mvc;
using Orion.AspNet.Endpoints;

namespace Orion.Manager.API.Common.Endpoints
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ApiEndpoint: AsyncEndpoint {}
}