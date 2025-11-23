// En: EjercicioGaelZarate.Application/DependencyInjection.cs

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR; // Nota: ya no necesitamos 'MediatR.Extensions.Microsoft.DependencyInjection'

namespace EjercicioGaelZarate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // --- ESTA LÍNEA ES LA QUE CAMBIA ---
            // Así se registra MediatR v12+
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            // Registra todos los Validators de FluentValidation (esto está bien)
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            return services;
        }
    }
}