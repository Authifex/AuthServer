using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authifex.Core.Interfaces;
using Authifex.DataAccess.Persistence;
using Authifex.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authifex.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services,
            string connectionString, bool isDevelopment)
        {
            services.AddDbContext<AuthifexDbContext>(opt =>
            {
                var serverVersion = ServerVersion.AutoDetect(connectionString);
                opt.UseMySql(connectionString, serverVersion,
                    b => b.MigrationsAssembly(typeof(AuthifexDbContext).Assembly.FullName));
                if (isDevelopment) opt.EnableSensitiveDataLogging();
            });

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IProfileRepository, ProfileRepository>();

            return services;
        }
    }
}
