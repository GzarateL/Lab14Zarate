using EjercicioGaelZarate.Application.Interfaces;
using EjercicioGaelZarate.Domain.Interfaces;
using EjercicioGaelZarate.Infrastructure.Persistence.Context;
using EjercicioGaelZarate.Infrastructure.Persistence.Repositories;
using EjercicioGaelZarate.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EjercicioGaelZarate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            // Configurar DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TicketeroDbContext>(options =>
                options.UseMySql(connectionString, 
                    ServerVersion.AutoDetect(connectionString)));

            // Registrar UnitOfWork y Repositorios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Registrar Servicios
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IJwtGenerator, JwtGenerator>();

            return services;
        }
    }
}