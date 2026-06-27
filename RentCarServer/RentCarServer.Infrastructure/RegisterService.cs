using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCarServer.Infrastructure.Context;
using Scrutor;

namespace RentCarServer.Infrastructure;

public static class RegisterService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration
        )
    {
        // Implementation for adding application services

        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            string con = configuration.GetConnectionString("DefaultConnection")!;
            opt.UseSqlServer(con);
        });

        services.Scan(action=>action
        .FromAssemblies(typeof(RegisterService).Assembly)
        .AddClasses(publicOnly:false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsImplementedInterfaces()
        .WithScopedLifetime());


        return services;
    }
}

