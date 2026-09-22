using App.Application.Common.Interfaces;
using App.Application.Features.FileRecords.Interfaces;
using App.Application.Features.FileRecords.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace App.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IFileRecordWriteService, FileRecordService>();
            service.AddScoped<IFileRecordReadService, FileRecordService>();

            return service;
        }
    }
}
