using App.Application.Features.FileRecords.DTO.Models;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.FileRecords.Interfaces
{
    public interface IFileRecordReadService
    {
        Task<IReadOnlyList<FileRecordModel>> GetFileRecordsAsync();
    }
}
