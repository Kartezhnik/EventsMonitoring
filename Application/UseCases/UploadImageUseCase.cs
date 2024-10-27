using Common.Models.Entities;
using Microsoft.IdentityModel.SecurityTokenService;


namespace Application.UseCases
{
    public class UploadImageUseCase
    {
        public static async Task UploadImageAsync(Event @event)
        {
            if (@event.ImageFile == null || @event.ImageFile.Length == 0)
                throw new BadRequestException("Изображение не загружено");

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
