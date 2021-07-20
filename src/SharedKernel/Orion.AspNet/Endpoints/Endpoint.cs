using Microsoft.AspNetCore.Mvc;

namespace Orion.AspNet.Endpoints
{
    public abstract class Endpoint
    {
        public abstract class WithRequest<TRequest>
        {
            public abstract class WithResponse<TResponse>: ControllerBase
            {
                public abstract TResponse Handle(TRequest request);
            }

            public abstract class WithoutResponse: ControllerBase
            {
                public abstract ActionResult Handle(TRequest request);
            }
        }

        public abstract class WithoutRequest
        {
            public abstract class WithResponse<TResponse>: ControllerBase
            {
                public abstract TResponse Handle();
            }

            public abstract class WithoutResponse: ControllerBase
            {
                public abstract ActionResult Handle();
            }
        }
    }
}