using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Validators
{
    public class ImageValidator
    {
        private static readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public void ValidateImage(IFormFile imageFile)
        {
            if (imageFile == null)
            {
                throw new ValidationException("Image file cannot be null.");
            }

            var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
            if (Array.IndexOf(_allowedExtensions, fileExtension) < 0)
            {
                throw new ValidationException("Invalid file type. Only JPG, JPEG, PNG, and GIF files are allowed.");
            }
        }
    }
}
