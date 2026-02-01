using EGeek.Identity.UseCases.User.Authenticate;
using EGeek.Identity.UseCases.User.CreateUser;
using EGeek.Identity.UseCases.User.GetUserByEmail;
using Microsoft.AspNetCore.Builder;

namespace EGeek.Identity.Configuration
{
    public static class IdentityConfiguration
    {
        public static void Apply(WebApplication app)
        {
            app.MapPost("/v1/user", CreateUserUseCase.Action);
            app.MapPost("/v1/authenticate", AutehenticateUseCase.Action);
            app.MapGet("/v1/getUserByEmail", GetUserByEmailUseCase.Action);
        }
    }
}
