using EjercicioGaelZarate.Application.DTOs;
using MediatR;

namespace EjercicioGaelZarate.Application.Features.Authentication.Commands.Login
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}