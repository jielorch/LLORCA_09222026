using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class FileRecord
    {
        public int Id { get; set; }
        public Guid PublicId { get; set; }
        public string FileName { get; set; } = null!;
        public TimeOnly ProcessingTime { get; set; }
    }
}
