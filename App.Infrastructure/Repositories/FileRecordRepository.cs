using App.Domain.Entities;
using App.Domain.Interfaces;
using App.Infrastructure.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Repositories
{
    public class FileRecordRepository(IAppDbContext context) : IFileRecordRepository
    {
       
        public async Task<IReadOnlyList<FileRecord>> GetFileRecordsAsync()
        {
            var result = await context.QueryAsync<FileRecord>("dbo.GetFileRecords", null, commandType: System.Data.CommandType.StoredProcedure);

            return [.. result];
        }

        public async Task<bool> UploadAsync(FileRecord fileRecord)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("FileName", fileRecord.FileName);
            dynamicParameters.Add("Average", fileRecord.Average);

            dynamicParameters.Add("Result", dbType: System.Data.DbType.Boolean, direction: System.Data.ParameterDirection.Output);

            await context.ExecuteAsync("dbo.SaveFileRecord", dynamicParameters, commandType: System.Data.CommandType.StoredProcedure);

            var isSuccess = dynamicParameters.Get<bool>("Result");

            return isSuccess;
        }
    }
}
