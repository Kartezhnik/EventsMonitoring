using Common;
using Common.Models.Entities;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.SecurityTokenService;


namespace Application.UseCases
{
    public class UserAuthorizationUseCase
    {
        ITokenService token;
        IRepository<User> repo;

        public string Authorizate(UserDto request, Context db)
        {
            if(request == null) throw new BadRequestException(nameof(request));
            var user = repo.GetByName(request.Name, db);

            if (user.Password == request.Password)
            {
                return token.GenerateJwtToken(request.Name);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
