using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Configs
{
    public class FileRecordConfiguration : IEntityTypeConfiguration<FileRecord>
    {
        public void Configure(EntityTypeBuilder<FileRecord> builder)
        {
            builder.ToTable("FileRecords", "dbo");

            builder.HasIndex(a => a.PublicId)
                .IsUnique()
                .IsClustered(false);

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FileName)
                .HasColumnType("NVARCHAR(255)")
                .IsRequired();



        }
    }
}
