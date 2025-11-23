using EjercicioGaelZarate.Application.DTOs;
using EjercicioGaelZarate.Application.Interfaces;
using EjercicioGaelZarate.Domain.Interfaces;
using MediatR;
using System; // Para UnauthorizedAccessException

namespace EjercicioGaelZarate.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar al usuario
            var user = await _unitOfWork.Users.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }

            // 2. Verificar la contraseña
            var isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }

            // 3. Generar el token
            var token = _jwtGenerator.GenerateToken(user);

            // 4. Devolver la respuesta
            return new AuthResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = token
            };
        }
    }
}