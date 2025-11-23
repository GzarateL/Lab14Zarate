using FluentValidation;

namespace EjercicioGaelZarate.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario es obligatorio.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}