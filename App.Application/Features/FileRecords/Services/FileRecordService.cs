using App.Application.Features.FileRecords.DTO.Models;
using App.Application.Features.FileRecords.DTO.Requests;
using App.Application.Features.FileRecords.Interfaces;
using App.Domain.Entities;
using App.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.FileRecords.Services
{
    public class FileRecordService(IFileRecordRepository fileRecordRepository) : IFileRecordWriteService, IFileRecordReadService
    {
        public async Task<IReadOnlyList<FileRecordModel>> GetFileRecordsAsync()
        {
            var result = await fileRecordRepository.GetFileRecordsAsync();

            var fileRecordModel = result.Select(f => new FileRecordModel
            {
                FileName = f.FileName,
                Average = f.Average,
                ProcessingTime = f.ProcessingTime,
                PublicId = f.PublicId
            });


            return [.. fileRecordModel];
        }

        public async Task<bool> UploadAsync(FileRequest fileRequest)
        {
            var fileRecord = new FileRecord
            {
                FileName = fileRequest.FileName,
                Average = fileRequest.Average
            };

            var result = await fileRecordRepository.UploadAsync(fileRecord);

            return result;
        }
    }
}
