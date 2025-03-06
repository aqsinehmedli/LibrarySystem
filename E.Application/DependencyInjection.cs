using AutoMapper;
using E.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using E.Application.PipelineBehaviours;
using FluentValidation;
using ManagementSystem.Application.Services.BackgroundServices;
//using E.Application.Services;
namespace E.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });


        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddHostedService<DeleteUserBackgroundService>();
        return services;
    }
}
