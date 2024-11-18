using Domain.Entities;


namespace Application.Validators
{
    public class UserValidator
    {
        public void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ValidationException("User cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ValidationException("User name is required.");
            }

            if (string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ValidationException("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
            {
                throw new ValidationException("Password must be at least 6 characters long.");
            }

            if (user.BirthdayDate == default)
            {
                throw new ValidationException("Birthday date is required.");
            }
        }
    }
}
