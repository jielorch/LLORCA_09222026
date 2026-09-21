using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Common.Interfaces
{
    public interface IApiKeyValidator
    {
        Task<bool> IsValidAsync(string apiKey);
    }
}
