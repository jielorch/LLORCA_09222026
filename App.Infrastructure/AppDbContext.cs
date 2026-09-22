using App.Domain.Entities;
using App.Infrastructure.Configs;
using App.Infrastructure.Interface;
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


        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.ApplyConfiguration(new FileRecordConfiguration());
        }
    }
}
