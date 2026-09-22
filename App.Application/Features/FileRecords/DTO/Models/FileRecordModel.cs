using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.FileRecords.DTO.Models
{
    public class FileRecordModel
    {
        public Guid PublicId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public decimal Average { get; set; }
        public TimeOnly ProcessingTime { get; set; }
    }
}
