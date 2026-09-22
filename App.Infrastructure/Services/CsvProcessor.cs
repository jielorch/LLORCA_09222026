using App.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Infrastructure.Services
{
    public partial class CsvProcessor : ICsvProcessor
    {
        // Using a partial Regex generator (optimized performance in modern .NET)
        [GeneratedRegex(@"[^\d.]")]
        private static partial Regex CleanCurrencyRegex();


        public async Task<decimal> CalculateAverageFromCsvAsync(Stream fileStream, string columnName)
        {
            decimal totalSum = 0;
            int rowCount = 0;
            int targetColumnIndex = -1;

            using var reader = new StreamReader(fileStream);

            var headerLine = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(headerLine))
            {
                throw new InvalidOperationException("The uploaded CSV file is empty.");
            }

            var headers = headerLine.Split(',');
            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Trim().Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    targetColumnIndex = i;
                    break;
                }
            }

            if (targetColumnIndex == -1)
            {
                throw new InvalidOperationException($"Column '{columnName}' was not found in the CSV header.");
            }

            while (await reader.ReadLineAsync() is { } line)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var columns = line.Split(',');
                if (targetColumnIndex >= columns.Length) continue;

                var rawValue = columns[targetColumnIndex].Trim();

                // Uses the compiler-generated source logic
                var sanitizedValue = CleanCurrencyRegex().Replace(rawValue, "");

                if (decimal.TryParse(sanitizedValue, CultureInfo.InvariantCulture, out decimal parsedAmount))
                {
                    totalSum += parsedAmount;
                    rowCount++;
                }
            }

            return rowCount > 0 ? Math.Round(totalSum / rowCount, 2) : 0;
        }
    }
}
