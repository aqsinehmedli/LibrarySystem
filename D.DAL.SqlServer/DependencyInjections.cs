using C.Repository.Common;
using D.DAL.SqlServer.Context;
using D.DAL.SqlServer.UnitOfWork.SqlUnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace D.DAL.SqlServer;

public static class DependencyInjections
{
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services,string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>(opt =>
        {
            var dbContext = opt.GetRequiredService<AppDbContext>();
            return new SqlUnitOfWork(connectionString, dbContext);
        });
        return services;
    }
}
