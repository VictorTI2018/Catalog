using EGeek.Identity.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EGeek.Identity.UseCases.User.CreateUser
{
    internal static class CreateUserUseCase
    {
        public static async Task<Created<string>> Action(
            CreateUserRequest request,
            UserManager<Entities.User> userManager)
        {

            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Password is required");

            if (string.IsNullOrEmpty(request.Role))
                throw new ArgumentException("Role is required");

            var user = new Entities.User(request.Email, request.Email);

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new EGeekIdentityException(result.Errors.First().Description);

            var roleClaim = new Claim(ClaimTypes.Role, request.Role);
            result = await userManager.AddClaimAsync(user, roleClaim);

            if (!result.Succeeded)
                throw new EGeekIdentityException(result.Errors.First().Description);

            return TypedResults.Created($"/user/{user.Id}", user.Id);
        }

    }
}
