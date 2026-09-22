using App.Application.Common.Interfaces;
using App.Domain.Interfaces;
using App.Infrastructure.Interface;
using App.Infrastructure.Repositories;
using App.Infrastructure.Security.Services;
using App.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            service.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            service.AddScoped<IApiKeyValidator, ApiKeyValidator>();
            service.AddScoped<IFileRecordRepository, FileRecordRepository>();
            service.AddScoped<ICsvProcessor, CsvProcessor>();

            return service;
        }
    }
}
