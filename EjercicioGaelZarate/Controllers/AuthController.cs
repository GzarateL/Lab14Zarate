using EjercicioGaelZarate.Application.Features.Authentication.Commands.Login;
using EjercicioGaelZarate.Application.Features.Authentication.Commands.Register;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioGaelZarate.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            // Mediator se encarga de buscar el RegisterCommandHandler
            var authResponse = await Mediator.Send(command);
            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            // Mediator se encarga de buscar el LoginCommandHandler
            var authResponse = await Mediator.Send(command);
            return Ok(authResponse);
        }
    }
}