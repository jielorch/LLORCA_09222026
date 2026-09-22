using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Common.Interfaces
{
    public interface ICsvProcessor
    {
        Task<decimal> CalculateAverageFromCsvAsync(Stream fileStream, string columnName);
    }
}
