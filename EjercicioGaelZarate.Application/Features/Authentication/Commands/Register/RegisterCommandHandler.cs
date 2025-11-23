using EjercicioGaelZarate.Application.DTOs;
using EjercicioGaelZarate.Application.Interfaces;
using EjercicioGaelZarate.Domain.Entities;
using EjercicioGaelZarate.Domain.Interfaces;
using MediatR;

namespace EjercicioGaelZarate.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtGenerator _jwtGenerator;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                throw new Exception("El nombre de usuario ya existe.");
            }

            string passwordHash = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                UserId = Guid.NewGuid(), // Corregido a Guid
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            string token = _jwtGenerator.GenerateToken(user);

            return new AuthResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = token
            };
        }
    }
}