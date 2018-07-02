using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a role, which a <see cref="User"/> will belong to.
    /// </summary>
    public class Role : IdentityRole<long>
    {
        public const string Clerk = nameof(Clerk);
        public const string Manager = nameof(Manager);
        public const string Pharmacist = nameof(Pharmacist);
    }
}