using Microsoft.AspNetCore.Identity;

namespace AerariumTech.Pharmacy.App.Models
{
    /// <inheritdoc />
    /// <summary>
    /// This class presents the authentication tokens of a <see cref="User"/>.
    /// </summary>
    public class UserToken : IdentityUserToken<long>
    {
    }
}