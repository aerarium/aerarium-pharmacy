using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// One of the many logins a user can have,
    /// when using external providers, such as Facebook, Google, etc
    /// </summary>
    public class UserLogin : IdentityUserLogin<long>
    {
    }
}