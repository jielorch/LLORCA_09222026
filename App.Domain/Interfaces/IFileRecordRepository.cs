using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Interfaces
{
    public interface IFileRecordRepository
    {
        Task<bool> UploadAsync(FileRecord fileRecord);

        Task<IReadOnlyList<FileRecord>> GetFileRecordsAsync();
    }
}
