using EGeek.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace EGeek.Identity.Entities
{
    public sealed class User
        : IdentityUser
    {
        private User() { }
        public User(
            string email,
            string name)
        {
            if (string.IsNullOrEmpty(email))
                throw new EGeekIdentityException("Email é obrigatório.");

            if (string.IsNullOrEmpty(name))
                throw new EGeekIdentityException("Nome é obrigatório.");

            Email = email;
            UserName = name;
        }
    }
}
