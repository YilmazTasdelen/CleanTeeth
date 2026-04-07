using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Persistance.Repository;
using CleanTeeth.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeeth.Persistance
{
    public static class RegisterPersistenceServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services
            ,IConfiguration configuration )
        {
            services.AddDbContext<CleanTeethDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDentalOfficeRepository, DentalOfficeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWorkEFCore>();

            return services;
        }
    }
}
