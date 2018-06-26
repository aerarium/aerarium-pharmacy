using AerariumTech.Pharmacy.Domain;

namespace AerariumTech.Pharmacy.Models.AccountViewModels
{
    public static class AccountConverter
    {
        public static User Convert(RegisterViewModel model)
            => new User
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email
            };
    }
}