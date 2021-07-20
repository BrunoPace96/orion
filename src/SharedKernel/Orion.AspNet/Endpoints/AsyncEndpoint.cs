using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Orion.AspNet.Endpoints
{
    public abstract class AsyncEndpoint
    {
        public abstract class WithRequest<TRouteQueryRequest, TBodyRequest>
        {
            public abstract class WithResponse<TResponse>: ControllerBase
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    TRouteQueryRequest routeQueryRequest,
                    TBodyRequest bodyRequest,
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse: ControllerBase
            {
                public abstract Task<ActionResult> HandleAsync(
                    TRouteQueryRequest routeQueryRequest,
                    TBodyRequest bodyRequest,
                    CancellationToken cancellationToken = default
                );
            }
        }
        
        public abstract class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse>: ControllerBase
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse: ControllerBase
            {
                public abstract Task<ActionResult> HandleAsync(
                    TRequest request,
                    CancellationToken cancellationToken = default
                );
            }
        }

        public abstract class WithoutRequest
        {
            public abstract class WithResponse<TResponse>: ControllerBase
            {
                public abstract Task<ActionResult<TResponse>> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }

            public abstract class WithoutResponse: ControllerBase
            {
                public abstract Task<ActionResult> HandleAsync(
                    CancellationToken cancellationToken = default
                );
            }
        }
    }
}