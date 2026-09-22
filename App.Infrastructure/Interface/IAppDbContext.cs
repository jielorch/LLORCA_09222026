using App.Domain.Entities;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App.Infrastructure.Interface
{
    public interface IAppDbContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        public DbSet<FileRecord> FileRecords { get; }

        Task<int> ExecuteAsync(string storedProcedure, DynamicParameters? parameters, CommandType commandType);
        Task<IReadOnlyList<T>> QueryAsync<T>(string storedProcedure, DynamicParameters? parameters, CommandType commandType);
    }
}
