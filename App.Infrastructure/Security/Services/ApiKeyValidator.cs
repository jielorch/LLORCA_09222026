using App.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infrastructure.Security.Services
{
    public class ApiKeyValidator(IConfiguration configuration) : IApiKeyValidator
    {
        private readonly IConfiguration _configuration = configuration;
        public Task<bool> IsValidAsync(string apiKey)
        {
            var expectedApiKey = _configuration["Authentication:ApiKey"];

            if (string.IsNullOrEmpty(expectedApiKey))
            {
                return Task.FromResult(false);
            }

            bool isValid = string.Equals(expectedApiKey, apiKey);

            return Task.FromResult(isValid);
        }
    }
}
