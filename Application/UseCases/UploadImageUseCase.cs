using EventsMonitoring.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class UploadImageUseCase
    {
        public async Task UploadImageAsync(IFormFile imageFile, Event @event)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Изображение не загружено");

            var uploadsDirectory = Path.Combine("uploads");

            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            @event.ImageUrl = $"/uploads/{fileName}";
        }
    }
}
