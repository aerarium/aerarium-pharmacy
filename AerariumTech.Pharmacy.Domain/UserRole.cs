using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// It is one of the many <see cref="Role"/>s a user can belong to.
    /// </summary>
    public class UserRole : IdentityUserRole<long>
    {
    }
}