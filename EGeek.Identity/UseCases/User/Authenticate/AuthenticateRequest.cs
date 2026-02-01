namespace EGeek.Identity.UseCases.User.Authenticate
{
    internal sealed record AuthenticateRequest(
        string Email,
        string Password
     );
}
