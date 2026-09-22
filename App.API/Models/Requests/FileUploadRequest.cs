using Microsoft.AspNetCore.Mvc;

namespace App.API.Models.Requests
{
    public class FileUploadRequest
    {
        // The [FromForm] attribute here tells the OpenAPI engine to treat this as form data fields
        [FromForm(Name = "File")]
        public IFormFile File { get; set; } = null!;
    }
}
