using App.Domain.Entities;
using App.Infrastructure.Configs;
using App.Infrastructure.Interface;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options), IAppDbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<FileRecord> FileRecords { get; set; }

        public async Task<int> ExecuteAsync(string storedProcedure, DynamicParameters? parameters, CommandType commandType)
            => await Connection.ExecuteAsync(storedProcedure, parameters, null, 90, commandType);


        public async Task<IReadOnlyList<T>> QueryAsync<T>(string storedProcedure, DynamicParameters? parameters, CommandType commandType)
        {
            var results = await Connection.QueryAsync<T>(storedProcedure, parameters, null, 90, commandType);
            return results.AsList();
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.ApplyConfiguration(new FileRecordConfiguration());
        }
    }
}
