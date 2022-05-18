using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        var useOnlyInMemoryDatabase = true;
        if (configuration["UseOnlyInMemoryDatabase"] != null)
        {
            useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]);
        }

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<CinemaContext>(c =>
               c.UseInMemoryDatabase("Cinema"));
        }
        else
        {
            services.AddDbContext<CinemaContext>(c =>
                c.UseSqlite(configuration.GetConnectionString("CinemaDB")));
        }
    }
}