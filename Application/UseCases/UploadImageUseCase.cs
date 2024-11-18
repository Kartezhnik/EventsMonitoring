using Application.Validators;
using Domain.Entities;
using Microsoft.IdentityModel.SecurityTokenService;


namespace Application.UseCases
{
    public class UploadImageUseCase
    {
        ImageValidator imageValidator;
        public UploadImageUseCase(ImageValidator _imageValidator)
        {
            imageValidator = _imageValidator;
        }
        public static async Task UploadImageAsync(Event @event)
        {
            var uploadsDirectory = Path.Combine("uploads");

            ImageValidator imageValidator = new ImageValidator();
            imageValidator.ValidateImage(@event.ImageFile);

            var fileName = $"{Guid.NewGuid()}_{@event.ImageFile.FileName}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await @event.ImageFile.CopyToAsync(stream);
                }
            }
            catch (ValidationException ex)
            {
                new BadRequestException(ex.Message);
            }
        }
    }
}
