using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// This class presents the authentication tokens of a <see cref="User"/>.
    /// </summary>
    public class UserToken : IdentityUserToken<long>
    {
    }
}