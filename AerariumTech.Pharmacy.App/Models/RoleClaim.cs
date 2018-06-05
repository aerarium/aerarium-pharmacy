using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.App.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the claims a <see cref="Role"/> possesses.
    /// </summary>
    public class RoleClaim : IdentityRoleClaim<long>
    {
    }
}