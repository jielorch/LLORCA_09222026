using App.Application.Features.FileRecords.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.FileRecords.Interfaces
{
    public interface IFileRecordWriteService
    {
        Task<bool> UploadAsync(FileRequest fileRequest);
    }
}
