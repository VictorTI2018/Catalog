namespace EGeek.Identity.UseCases.User.CreateUser
{
    internal sealed class CreateUserRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
