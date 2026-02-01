namespace EGeek.Identity.UseCases.User.GetUserByEmail
{
    internal sealed class GetUserByEmailResponse
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }
    }
}
