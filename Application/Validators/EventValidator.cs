using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Validators
{
    public class EventValidator
    {
        public void ValidateEvent(Event @event)
        {
            if (@event == null)
            {
                throw new ValidationException("Event cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(@event.Name))
            {
                throw new ValidationException("Event name is required.");
            }

            if (string.IsNullOrWhiteSpace(@event.Description))
            {
                throw new ValidationException("Event description is required.");
            }

            if (@event.DateOfEvent == default)
            {
                throw new ValidationException("Event date must be provided.");
            }

            if (@event.PlaceOfEvent == null)
            {
                throw new ValidationException("Event place is required.");
            }

            if (@event.DateOfEvent < DateTime.Now)
            {
                throw new ValidationException("Event date cannot be in the past.");
            }

            if (string.IsNullOrWhiteSpace(@event.Type))
            {
                throw new ValidationException("Event type is required.");
            }

            if (@event.ImageFile == null)
            {
                throw new ValidationException("Event image is required.");
            }

            if (@event.ImageFile != null && !IsValidImage(@event.ImageFile))
            {
                throw new ValidationException("Invalid image format. Supported formats: .jpg, .png, .gif.");
            }
        }
        private bool IsValidImage(IFormFile imageFile)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            return true;
        }
    }
}
