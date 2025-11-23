using MediatR; // <-- ESTA LÍNEA FALTABA
using Microsoft.AspNetCore.Mvc;

namespace EjercicioGaelZarate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        // Una versión 'lazy' de MediatR para no inyectarlo en cada controller
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}