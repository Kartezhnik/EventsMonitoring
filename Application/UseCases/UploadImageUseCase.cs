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
        public static async Task UploadImageAsync(Event @event)
        {
            if (@event.ImageFile == null || @event.ImageFile.Length == 0)
                throw new ArgumentException("Изображение не загружено");

            var uploadsDirectory = Path.Combine("uploads");

            var fileName = $"{Guid.NewGuid()}_{@event.ImageFile.FileName}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await @event.ImageFile.CopyToAsync(stream);
            }
        }
    }
}
