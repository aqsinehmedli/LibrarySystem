using AutoMapper;
using E.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
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
        return services;
    }
}
