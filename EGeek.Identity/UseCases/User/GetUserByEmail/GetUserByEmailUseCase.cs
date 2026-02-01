using EGeek.Identity.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EGeek.Identity.UseCases.User.GetUserByEmail
{
    internal sealed class GetUserByEmailUseCase
    {
        [Authorize]
        public static async Task<Ok<GetUserByEmailResponse>> Action(
            ClaimsPrincipal claimsPrincipal,
            UserManager<EGeek.Identity.Entities.User> userManager)
        {
            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            if (email is null)
                throw new EGeekIdentityException("Claim email is not found.");

            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
                throw new EGeekIdentityException("User not found.");

            var claims = await userManager.GetClaimsAsync(user);
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            return TypedResults.Ok(new GetUserByEmailResponse
            {
                Email = user.Email ?? string.Empty,
                Name = user.UserName ?? string.Empty,
                Role = role ?? string.Empty,
            });
        }
    }
}
