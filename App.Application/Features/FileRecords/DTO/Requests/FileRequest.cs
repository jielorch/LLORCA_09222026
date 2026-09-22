using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Features.FileRecords.DTO.Requests
{
    public class FileRequest
    {
         
        public string FileName { get; set; } = null!;
        public decimal Average { get; set; }
    }
}
