using CberTest.WebApi.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CberTest.WebApi.Models
{
    public class CreatePhotoBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        // TODO: Вынести список разрешенных расщирений
        [AllowedExtensions(new string[] {".jpeg", ".jpg", ".png", ".tiff", ".gif", ".bmp"})]
        public IFormFile File { get; set; }
    }
}
