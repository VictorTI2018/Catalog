using EGeek.Identity.Configuration;
using EGeek.Identity.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EGeek.Identity.UseCases.User.Authenticate
{
    internal sealed class AutehenticateUseCase
    {
        public static async Task<Results<Ok<string>, UnauthorizedHttpResult>> Action(
            AuthenticateRequest request,
            UserManager<EGeek.Identity.Entities.User> userManager,
            IOptions<AuthenticateOptions> options)
        {
            var authenticaOptions = options.Value;

            if (string.IsNullOrEmpty(request.Email))
                throw new EGeekIdentityException("Email é obrigatório.");

            if (string.IsNullOrEmpty(request.Password))
                throw new EGeekIdentityException("Password é obrigatório.");

            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return TypedResults.Unauthorized();

            var result = await userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
                return TypedResults.Unauthorized();


            return TypedResults.Ok(GenerateToken(authenticaOptions, request.Email));

        }

        private static string GenerateToken(
            AuthenticateOptions options,
            string email)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var algoritm = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(options.Issuer,
                options.Audience,
                [new Claim(ClaimTypes.Email, email)],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: algoritm);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
