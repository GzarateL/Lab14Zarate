using EjercicioGaelZarate.Application.DTOs;
using MediatR;

namespace EjercicioGaelZarate.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}