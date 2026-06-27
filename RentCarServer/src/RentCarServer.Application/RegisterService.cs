using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RentCarServer.Application.Behaviours;
using TS.MediatR;

namespace RentCarServer.Application;

public static  class RegisterService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Implementation for adding application services

        services.AddMediatR(cfr =>
        {
            cfr.RegisterServicesFromAssembly(typeof(RegisterService).Assembly);
            cfr.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfr.AddOpenBehavior(typeof(PermissionBehavior<,>));

        });


        services.AddValidatorsFromAssembly(typeof(RegisterService).Assembly);
        return services;
    }
}
