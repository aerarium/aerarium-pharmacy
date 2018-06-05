using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.App.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a role, which a <see cref="User"/> will belong to.
    /// </summary>
    public class Role : IdentityRole<long>
    {
    }
}